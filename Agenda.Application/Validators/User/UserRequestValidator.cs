using Agenda.Application.ViewModels.User;
using FluentValidation;

namespace Agenda.Application.Validators.User
{
    public class UserRequestValidator : AbstractValidator<UserRequest>
    {

        public UserRequestValidator()
        {
            RuleFor(u => u.Name)
                .UserFullName();

            RuleFor(u => u.Email)
                .UserEmail();

            RuleFor(u => u.Username)
                .Username();

            RuleFor(u => u.Password)
                .UserPassword();

            RuleFor(u => u.RoleId)
                .UserRole();
        }

    }
}
