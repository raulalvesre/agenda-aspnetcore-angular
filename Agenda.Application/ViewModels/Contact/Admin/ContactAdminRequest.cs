using System.Threading.Tasks;
using Agenda.Application.Validators.Contact;
using Agenda.Application.ViewModels.Contact.Base;
using FluentValidation.Results;



namespace Agenda.Application.ViewModels.Contact
{
    public class ContactAdminRequest : ContactRequestBase
    {

        public int OwnerId { get; set; }

        public Task<ValidationResult> ValidateAsync()
        {
            return new ContactAdminRequestValidator().ValidateAsync(this);
        }

    }
}
