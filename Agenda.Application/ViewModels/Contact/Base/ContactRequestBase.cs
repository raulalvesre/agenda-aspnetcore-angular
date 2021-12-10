using System.Collections.Generic;
using Agenda.Application.ViewModels.ContactTelephone;

namespace Agenda.Application.ViewModels.Contact.Base
{
    public abstract class ContactRequestBase
    {
        public string Name { get; set; }
        public List<ContactTelephoneRequest> Telephones { get; set; }

    }
}
