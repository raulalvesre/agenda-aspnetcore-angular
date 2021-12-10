using System.Linq;
using System.Text.RegularExpressions;
using Agenda.Domain.Core;
using Agenda.Domain.Models.Types;
using FluentValidation;

namespace Agenda.Application.Validators.ContactTelephone
{
    public static class ContactTelephoneValidatorExtensions
    {

        public static IRuleBuilderOptions<T, string> TelephoneDescription<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
               .NotEmpty()
                    .WithMessage("A descrição não pode ser vazia")
               .MaximumLength(50)
                    .WithMessage("A descrição deve ter no máximo 50 caracteres");
        }

        public static IRuleBuilderOptions<T, string> CellphoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var cellphoneRegex = @"^\((?:[14689][1-9]|2[12478]|3[1234578]|5[1345]|7[134579])\) 9[1-9][0-9]{3}\-[0-9]{4}$";

            return ruleBuilder
                .Must(t => Regex.IsMatch(t, cellphoneRegex))
                .WithMessage("Número de celular inválido");
        }

        public static IRuleBuilderOptions<T, string> CommercialTelephoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var commercialRegex = @"^\((?:[14689][1-9]|2[12478]|3[1234578]|5[1345]|7[134579])\) (?:[2-8]|9[1-9])[0-9]{3}\-[0-9]{4}$";

            return ruleBuilder
                .Must(t => Regex.IsMatch(t, commercialRegex))
                .WithMessage("Número de telefone inválido");
        }

        public static IRuleBuilderOptions<T, string> LandlineTelephoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var landLineRegex = @"^\((?:[14689][1-9]|2[12478]|3[1234578]|5[1345]|7[134579])\) [2-8][0-9]{3}\-[0-9]{4}$";

            return ruleBuilder
                .Must(t => Regex.IsMatch(t, landLineRegex))
                .WithMessage("Número de telefone fixo inválido");
        }

        public static IRuleBuilderOptions<T, int> TelephoneType<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder
               .NotEmpty()
               .GreaterThan(0)
                   .WithMessage("Não existe um tipo de telefone com esse ID")
               .LessThanOrEqualTo(GetHowManyTelephoneTypesExist())
                   .WithMessage("Não existe um tipo de telefone com esse ID");
        }

        private static int GetHowManyTelephoneTypesExist()
        {
            return Enumeration.GetAll<TelephoneType>().Count();
        }

    }
}
