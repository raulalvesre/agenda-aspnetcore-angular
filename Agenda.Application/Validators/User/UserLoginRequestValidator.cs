using Agenda.Application.ViewModels.User;
using FluentValidation;

namespace Agenda.Application.Validators.User
{
    public class UserLoginRequestValidator : AbstractValidator<UserLoginRequest>
    {

        public UserLoginRequestValidator()
        {
            RuleFor(u => u.Username)
                .NotEmpty()
                    .WithMessage("O nome não pode ser vazio");

            RuleFor(u => u.Password)
                .NotEmpty()
                    .WithMessage("A senha não pode estar vazia");
        }

    }
}
