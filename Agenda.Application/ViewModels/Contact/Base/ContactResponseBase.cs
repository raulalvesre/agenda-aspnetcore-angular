using System.Collections.Generic;
using Agenda.Application.ViewModels.ContactTelephone;

namespace Agenda.Application.ViewModels.Contact.Base
{
    public abstract class ContactResponseBase : RecordViewModel
    {
        public string Name { get; set; }
        public List<ContactTelephoneResponse> Telephones { get; set; }

    }
}
