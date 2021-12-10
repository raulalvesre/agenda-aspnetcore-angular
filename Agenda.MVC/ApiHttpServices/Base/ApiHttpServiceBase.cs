using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.MVC.ViewModels;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Agenda.MVC.ApiHttpServices.Base
{
    public abstract class ApiHttpServiceBase
    {

        protected readonly string _apiUrl = "https://localhost:5001/api";
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly string JWT_TOKEN;

        public ApiHttpServiceBase(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            JWT_TOKEN = httpContextAccessor.HttpContext.User.Claims.First(c => c.Type.Equals("jwt-token")).Value;
        }
    }

}
