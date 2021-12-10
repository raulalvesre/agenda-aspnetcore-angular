using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Agenda.MVC.ViewModels.User.ValidatorAttributes
{
    public class UsernameMinimumLenghtValidatorAttribute : StringLengthAttribute, IClientModelValidator
    {

        public UsernameMinimumLenghtValidatorAttribute(int maximumLength) : base(maximumLength) { }

        public override bool IsValid(object value)
        {
            string val = Convert.ToString(value);

            if (val.Length < base.MinimumLength)
                base.ErrorMessage = "O username tem que ter no mínimo 3 caracteres";

            return base.IsValid(value);
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-username-with-correct-minimum-length", "O username tem que ter no mínimo 3 caracteres");
        }

    }
}
