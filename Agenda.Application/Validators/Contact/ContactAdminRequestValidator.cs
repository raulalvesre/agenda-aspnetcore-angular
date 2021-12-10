using Agenda.Application.Validators.Contact.Base;
using Agenda.Application.ViewModels.Contact;
using FluentValidation;

namespace Agenda.Application.Validators.Contact
{
    public class ContactAdminRequestValidator : ContactRequestBaseValidator<ContactAdminRequest>
    {
        public ContactAdminRequestValidator() : base()
        {
            RuleFor(cR => cR.OwnerId)
                .NotEmpty();
        }
    }
}
