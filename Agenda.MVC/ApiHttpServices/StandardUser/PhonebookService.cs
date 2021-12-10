using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.MVC.ApiHttpServices.Base;
using Agenda.MVC.ViewModels;
using Agenda.MVC.ViewModels.ApiResponse;
using Agenda.MVC.ViewModels.Contact;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Agenda.MVC.ApiHttpServices
{
    public class PhonebookService : ApiHttpServiceBase
    {
        public PhonebookService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }

        public async Task<ApiPaginationResponse<IEnumerable<ContactViewModel>>> GetContactPage(PhonebookSearchViewModel searchParameters)
        {
            int skip = searchParameters.Page != 0 ? 7 * (searchParameters.Page - 1) : 0;

            var response = await _apiUrl
                .WithOAuthBearerToken(JWT_TOKEN)
                .AllowAnyHttpStatus()
                .AppendPathSegment("/agenda/buscar")
                .SetQueryParams(new
                {
                    searchParameters.IdContato,
                    searchParameters.NomeContato,
                    searchParameters.Ddd,
                    searchParameters.NumeroTelefone,
                    skip,
                    take = 7
                })
                .GetJsonAsync<ApiPaginationResponse<IEnumerable<ContactViewModel>>>();

            return response;
        }

        public async Task<ContactViewModel> GetContact(int id)
        {
            var contacts = await _apiUrl
                .WithOAuthBearerToken(JWT_TOKEN)
                .AllowAnyHttpStatus()
                .AppendPathSegment("/agenda")
                .AppendPathSegment(id)
                .GetJsonAsync<ContactViewModel>();

            contacts.Telephones.Select(async t => { t.TypeId = await GetTelephoneTypeIdFromTelephoneTypeString(t.Type); return t; }).ToList();

            return contacts;
        }

        public async Task<ApiResponseViewModel<ContactViewModel>> AddContact(ContactFormViewModel newContact)
        {
            var response = await _apiUrl
                .WithOAuthBearerToken(JWT_TOKEN)
                .AllowAnyHttpStatus()
                .AppendPathSegment("agenda")
                .PostJsonAsync(newContact)
                .GetApiResponse<ContactViewModel>();

            return response;
        }

        public async Task<ApiResponseViewModel<ContactViewModel>> UpdateContact(ContactFormViewModel updatedContact)
        {
            var response = await _apiUrl
                .WithOAuthBearerToken(JWT_TOKEN)
                .AllowAnyHttpStatus()
                .AppendPathSegment("agenda")
                .AppendPathSegment(updatedContact.Id)
                .PutJsonAsync(updatedContact)
                .GetApiResponse<ContactViewModel>();

            return response;
        }

        public async Task RemoveContact(int id)
        {
            await _apiUrl
                .WithOAuthBearerToken(JWT_TOKEN)
                .AllowAnyHttpStatus()
                .AppendPathSegment("agenda")
                .AppendPathSegment(id)
                .DeleteAsync();
        }

        public async Task<IEnumerable<TypeViewModel>> GetTelephoneTypes()
        {
            return await _apiUrl
                .WithOAuthBearerToken(JWT_TOKEN)
                .AllowAnyHttpStatus()
                .AppendPathSegment("agenda")
                .AppendPathSegment("tipos-telefone")
                .GetJsonAsync<IEnumerable<TypeViewModel>>();
        }

        private async Task<int> GetTelephoneTypeIdFromTelephoneTypeString(string telephoneType)
        {
            return (await GetTelephoneTypes()).First(tt => tt.Name.Equals(telephoneType)).Id;
        }

    }
}
