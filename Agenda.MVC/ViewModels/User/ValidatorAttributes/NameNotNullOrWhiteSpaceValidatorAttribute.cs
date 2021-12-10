using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Agenda.MVC.ViewModels.User
{
    public class NameNotNullOrWhiteSpaceValidatorAttribute : ValidationAttribute, IClientModelValidator
    {

        public NameNotNullOrWhiteSpaceValidatorAttribute() : base("Campo inválido") { }
        public NameNotNullOrWhiteSpaceValidatorAttribute(string message) : base(message) { }

        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            if (string.IsNullOrWhiteSpace(value.ToString()))
                return false;

            return true;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-name-not-null-or-empty", "O campo Nome não pode estar vazio");
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (IsValid(value))
                return ValidationResult.Success;

            return new ValidationResult("O nome não pode ser vazio");
        }

    }
}
