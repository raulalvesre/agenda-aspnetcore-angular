using System.Threading.Tasks;
using Agenda.Application.Validators.User;
using FluentValidation.Results;

namespace Agenda.Application.ViewModels.User
{
    public class UserRequest
    {

        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public Task<ValidationResult> ValidateAsync()
        {
            return new UserRequestValidator().ValidateAsync(this);
        }

    }
}
