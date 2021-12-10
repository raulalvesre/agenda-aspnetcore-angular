using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.MVC.ApiHttpServices.Base;
using Agenda.MVC.ViewModels;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Agenda.MVC.ApiHttpServices
{
    public class LoginService
    {

        private readonly string _apiUrl = "https://localhost:5001/api";
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoginService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> Login(LoginViewModel loginVm)
        {
            var response = await _apiUrl
                .AllowAnyHttpStatus()
                .AppendPathSegment("login")
                .PostJsonAsync(loginVm);

            if (response.StatusCode == 401)
                return null;

            var result = await response.GetJsonAsync();

            return result.token as string;
        }



    }
}
