using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Agenda.MVC.ApiHttpServices;
using Agenda.MVC.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.MVC.Controllers
{

    public class LoginController : Controller
    {

        private readonly LoginService _apiHttpService;

        public LoginController(LoginService apiHttpService)
        {
            _apiHttpService = apiHttpService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (Request.Cookies["HasLoggedIn"] != null)
            {
                TempData.Add("toast", "Sua sessão expirou");
                Response.Cookies.Delete("HasLoggedIn");
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(LoginViewModel loginViewModel)
        {
            var token = await _apiHttpService.Login(loginViewModel);

            if (token == null)
            {
                ModelState.AddModelError(string.Empty, "Username ou senha inválidos");
                return View(loginViewModel);
            }

            var decodedToken = DecodeToken(token);
            var claims = decodedToken.Claims.Concat(new[] { new Claim("jwt-token", token) });
            var cookieClaims = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, "unique-name", "role");

            Response.Cookies.Append("HasLoggedIn", "true");

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(cookieClaims),
                GetAuthProperties());

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            Response.Cookies.Delete("HasLoggedIn");
            return Redirect(Url.Action("Index"));
        }

        private JwtSecurityToken DecodeToken(string jwtToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(jwtToken);
            var decodedToken = jsonToken as JwtSecurityToken;

            return decodedToken;
        }

        private AuthenticationProperties GetAuthProperties()
        {
            return new()
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1),
                IsPersistent = true,
                RedirectUri = "/"
            };
        }

    }
}
