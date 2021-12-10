using Agenda.Application.ViewModels.ContactTelephone;
using FluentValidation;

namespace Agenda.Application.Validators.ContactTelephone
{
    public class ContactTelephoneRequestValidator : AbstractValidator<ContactTelephoneRequest>
    {

        public ContactTelephoneRequestValidator()
        {
            RuleFor(t => t.Description)
                .TelephoneDescription();

            RuleFor(t => t.TelephoneNumber)
                .LandlineTelephoneNumber()
                    .When(t => t.Type == 1, ApplyConditionTo.CurrentValidator)
                .CommercialTelephoneNumber()
                    .When(t => t.Type == 2, ApplyConditionTo.CurrentValidator)
                .CellphoneNumber()
                    .When(t => t.Type == 3, ApplyConditionTo.CurrentValidator);

            RuleFor(t => t.Type)
                .TelephoneType();
        }

    }
}
