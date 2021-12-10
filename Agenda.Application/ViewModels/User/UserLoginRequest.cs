using System.Threading.Tasks;
using Agenda.Application.Validators.User;
using FluentValidation.Results;

namespace Agenda.Application.ViewModels.User
{
    public class UserLoginRequest
    {

        public string Username { get; set; }
        public string Password { get; set; }

        public Task<ValidationResult> ValidateAsync()
        {
            return new UserLoginRequestValidator().ValidateAsync(this);
        }

    }
}
