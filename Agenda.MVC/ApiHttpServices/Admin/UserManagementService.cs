using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.MVC.ApiHttpServices.Base;
using Agenda.MVC.ViewModels;
using Agenda.MVC.ViewModels.ApiResponse;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
namespace Agenda.MVC.ApiHttpServices
{
    public class UserManagementService : ApiHttpServiceBase
    {

        public UserManagementService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        public async Task<ApiPaginationResponse<IEnumerable<UserViewModel>>> GetUserPage(int page)
        {
            int skip = page != 0 ? 6 * (page - 1) : 0;

            var response = await _apiUrl
                .WithOAuthBearerToken(JWT_TOKEN)
                .AllowAnyHttpStatus()
                .AppendPathSegment("/admin/usuarios/buscar")
                .SetQueryParams(new
                {
                    skip,
                    take = 6
                })
                .GetJsonAsync<ApiPaginationResponse<IEnumerable<UserViewModel>>>();

            return response;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsers()
        {
            var response = await _apiUrl
                            .WithOAuthBearerToken(JWT_TOKEN)
                            .AllowAnyHttpStatus()
                            .AppendPathSegment("/admin/usuarios/buscar")
                            .GetJsonAsync<ApiPaginationResponse<IEnumerable<UserViewModel>>>();

            return response.Data;
        }

        public async Task<UserViewModel> GetUser(int id)
        {
            var response = await _apiUrl
                .WithOAuthBearerToken(JWT_TOKEN)
                .AllowAnyHttpStatus()
                .AppendPathSegment("/admin/usuarios/")
                .AppendPathSegment(id)
                .GetJsonAsync<UserViewModel>();

            var user = response;

            if (user != null)
                user.RoleId = await GetRoleIdFromRoleString(user.Role);

            return user;
        }

        public async Task<ApiResponseViewModel<UserViewModel>> AddUser(UserFormViewModel newUser)
        {
            return await _apiUrl
                 .WithOAuthBearerToken(JWT_TOKEN)
                 .AllowAnyHttpStatus()
                 .AppendPathSegment("/admin/usuarios")
                 .PostJsonAsync(newUser)
                 .GetApiResponse<UserViewModel>();
        }

        public async Task<ApiResponseViewModel<UserViewModel>> UpdateUser(UserFormViewModel updatedUser)
        {
            return await _apiUrl
                .WithOAuthBearerToken(JWT_TOKEN)
                .AllowAnyHttpStatus()
                .AppendPathSegment("/admin/usuarios")
                .AppendPathSegment(updatedUser.Id)
                .PutJsonAsync(updatedUser)
                .GetApiResponse<UserViewModel>();
        }

        public async Task RemoveUser(int id)
        {
            await _apiUrl
                .WithOAuthBearerToken(JWT_TOKEN)
                .AllowAnyHttpStatus()
                .AppendPathSegment("/admin/usuarios")
                .AppendPathSegment(id)
                .DeleteAsync();
        }

        public async Task<IEnumerable<TypeViewModel>> GetUserRoles()
        {
            return await _apiUrl
                .WithOAuthBearerToken(JWT_TOKEN)
                .AllowAnyHttpStatus()
                .AppendPathSegment("/admin/usuarios/tipos")
                .GetJsonAsync<IEnumerable<TypeViewModel>>();
        }

        private async Task<int> GetRoleIdFromRoleString(string role)
        {
            return (await GetUserRoles()).First(uR => uR.Name.Equals(role)).Id;
        }

    }
}
