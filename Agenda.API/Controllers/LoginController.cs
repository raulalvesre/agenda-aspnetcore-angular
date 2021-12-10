using System.Threading.Tasks;
using Agenda.API.Auth;
using Agenda.Application.Interfaces;
using Agenda.Application.ViewModels;
using Agenda.Application.ViewModels.Exceptions.Base;
using Agenda.Application.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.API.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(TokenViewModel), 200)]
        [ProducesResponseType(typeof(ExceptionViewModel), 401)]
        public async Task<ActionResult<TokenViewModel>> GetToken(UserLoginRequest user)
        {
            var userResponse = await _loginService.Login(user);
            var token = new TokenViewModel()
            {
                Token = TokenService.GenerateToken(userResponse)
            };

            return Ok(token);
        }

    }
}
