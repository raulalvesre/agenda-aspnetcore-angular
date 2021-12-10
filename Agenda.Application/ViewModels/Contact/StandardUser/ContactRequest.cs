using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Agenda.Application.Validators.Contact;
using Agenda.Application.ViewModels.Contact.Base;
using FluentValidation.Results;

namespace Agenda.Application.ViewModels.Contact
{
    public class ContactRequest : ContactRequestBase
    {

        public Task<ValidationResult> ValidateAsync()
        {
            return new ContactRequestValidator().ValidateAsync(this);
        }

    }

}
