using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Agenda.MVC.ViewModels.User.ValidatorAttributes
{
    public class PasswordWithAtLeastOneNumberValidatorAttribute : ValidationAttribute, IClientModelValidator
    {

        public PasswordWithAtLeastOneNumberValidatorAttribute() : base("Campo inválido") { }
        public PasswordWithAtLeastOneNumberValidatorAttribute(string message) : base(message) { }

        public override bool IsValid(object value)
        {
            string password = Convert.ToString(value);

            return password.Any(char.IsDigit);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (IsValid(value))
                return ValidationResult.Success;

            return new ValidationResult("A senha deve ter pelo menos um número");
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val-password-with-at-least-one-number", "A senha deve ter pelo menos um número");
        }

    }
}
