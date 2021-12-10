using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Agenda.MVC.ViewModels.ContactTelephone.ValidatorAttributes
{
    public class ContactTelephoneNumberValidatorAttribute : ValidationAttribute, IClientModelValidator
    {

        public ContactTelephoneNumberValidatorAttribute() : base("Campo inválido") { }
        public ContactTelephoneNumberValidatorAttribute(string message) : base(message) { }

        public override bool IsValid(object value)
        {
            Regex telRegex = new Regex(@"^\((?:[14689][1-9]|2[12478]|3[1234578]|5[1345]|7[134579])\) (?:[2-8]|9[1-9])[0-9]{3}\-[0-9]{4}$");

            if (value == null)
                return false;

            return telRegex.IsMatch((string)value);
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-telephone-number", "Número de telefone inválido");
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Número de telefone inválido");

            if (IsValid(value))
                return ValidationResult.Success;

            return new ValidationResult("Número de telefone inválido");
        }

    }
}
