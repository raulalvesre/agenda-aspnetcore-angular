using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Agenda.Application.Validators.ContactTelephone;
using FluentValidation.Results;

namespace Agenda.Application.ViewModels.ContactTelephone
{
    public class ContactTelephoneRequest
    {

        [JsonIgnore]
        public int ContactId { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public string TelephoneNumber { get; set; }

        public Task<ValidationResult> ValidateAsync()
        {
            return new ContactTelephoneRequestValidator().ValidateAsync(this);
        }

    }
}
