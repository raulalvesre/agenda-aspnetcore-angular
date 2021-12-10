using System.Linq;
using Agenda.Domain.Core;
using Agenda.Domain.Models.Types;
using FluentValidation;

namespace Agenda.Application.Validators.User
{
    public static class UserValidatorExtensions
    {

        public static IRuleBuilderOptions<T, string> UserFullName<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
               .NotEmpty()
                    .WithMessage("O nome não pode ser vazio")
               .MaximumLength(200)
                    .WithMessage("O nome deve ter no máximo 200 caracteres");
        }

        public static IRuleBuilderOptions<T, string> UserEmail<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
               .NotEmpty()
                    .WithMessage("É necessário um email")
                .EmailAddress()
                    .WithMessage("O email tem que ser válido");
        }

        public static IRuleBuilderOptions<T, string> Username<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
               .NotEmpty()
                    .WithMessage("É necessário um username")
                .MinimumLength(3)
                    .WithMessage("O username tem que ter no mínimo 3 caracteres")
                .MaximumLength(16)
                    .WithMessage("O username tem que ter no máximo 16 caracteres");
        }

        public static IRuleBuilderOptions<T, int> UserRole<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder
               .NotEmpty()
                    .WithMessage("É necessário uma USER ROLE")
               .GreaterThan(0)
                    .WithMessage("Não existe USER ROLE com esse ID")
               .LessThanOrEqualTo(GetHowManyUserRolesExist())
                    .WithMessage("Não existe USER ROLE com esse ID");
        }

        public static IRuleBuilderOptions<T, string> UserPassword<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .MinimumLength(6)
                    .WithMessage("A senha deve possuir no mínimo 6 caracteres")
                .MaximumLength(30)
                    .WithMessage("A senha deve possuir no máximo 30 caracteres")
                .Must(PasswordContainsAtLeastOneNumber)
                    .WithMessage("A senha deve possuir pelo menos um número");
        }

        private static int GetHowManyUserRolesExist()
        {
            return Enumeration.GetAll<UserRole>().Count();
        }

        private static bool PasswordContainsAtLeastOneNumber(string password)
        {
            return password.Any(char.IsDigit);
        }

    }
}
