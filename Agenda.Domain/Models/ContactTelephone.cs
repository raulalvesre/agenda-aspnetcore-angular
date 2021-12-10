using Agenda.Domain.Core;
using Agenda.Domain.Models.Types;

namespace Agenda.Domain.Models
{
    public class ContactTelephone : Record
    {

        public string Description { get; set; }
        public int Ddd { get; set; }
        public string TelephoneOnlyNumbers { get; set; }
        public string TelephoneFormatted { get; set; }

        public int ContactId { get; set; }
        public Contact Contact { get; set; }
        public int TelephoneTypeId { get; set; }
        public TelephoneType TelephoneType { get; set; }

    }
}
