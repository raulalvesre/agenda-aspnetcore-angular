using System.Text.Json.Serialization;

namespace Agenda.Application.ViewModels.ContactTelephone
{
    public class ContactTelephoneResponse : RecordViewModel
    {

        [JsonIgnore]
        public int ContactId { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int Ddd { get; set; }
        public string TelephoneOnlyNumbers { get; set; }
        public string TelephoneFormatted { get; set; }

    }
}
