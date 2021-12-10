using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Agenda.MVC.ViewModels.User.ValidatorAttributes
{
    public class PasswordMinimumLenghtValidatorAttribute : StringLengthAttribute, IClientModelValidator
    {

        public PasswordMinimumLenghtValidatorAttribute(int maximumLenght) : base(maximumLenght) { }

        public override bool IsValid(object value)
        {
            string val = Convert.ToString(value);

            if (val.Length < base.MinimumLength)
                base.ErrorMessage = "A senha deve ter no mínimo 6 caracteres";

            return base.IsValid(value);
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-password-with-correct-minimum-length", "A senha deve ter no mínimo 6 caracteres");
        }

    }
}
