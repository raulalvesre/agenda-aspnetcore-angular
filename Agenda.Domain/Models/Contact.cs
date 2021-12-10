using System.Collections.Generic;

using Agenda.Domain.Core;

namespace Agenda.Domain.Models
{
    public class Contact : Record
    {

        public string Name { get; set; }
        public IEnumerable<ContactTelephone> Telephones { get; set; }
        public int OwnerId { get; set; }
        public User Owner { get; set; }

    }
}
