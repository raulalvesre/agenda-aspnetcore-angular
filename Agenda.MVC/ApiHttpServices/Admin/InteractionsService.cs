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
    public class InteractionsService : ApiHttpServiceBase
    {
        public InteractionsService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        public async Task<ApiPaginationResponse<IEnumerable<InteractionViewModel>>> GetInteractionPage(int page)
        {
            int skip = page != 0 ? 6 * (page - 1) : 0;

            var response = await _apiUrl
                .WithOAuthBearerToken(JWT_TOKEN)
                .AllowAnyHttpStatus()
                .AppendPathSegment("/admin/interacoes/buscar")
                .SetQueryParams(new
                {
                    skip = skip,
                    take = 6
                })
                .GetJsonAsync<ApiPaginationResponse<IEnumerable<InteractionViewModel>>>();

            return response;
        }

    }
}
