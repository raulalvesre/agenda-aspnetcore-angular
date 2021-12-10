using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.MVC.ViewModels.ContactTelephone
{
    public class ContactTelephoneViewModel : RecordViewModel
    {

        [DisplayName("Tipo")]
        public string Type { get; set; }
        public int TypeId { get; set; }

        [Required]
        [DisplayName("Descrição")]
        public string Description { get; set; }

        public int Ddd { get; set; }
        public string TelephoneOnlyNumbers { get; set; }
        public string TelephoneFormatted { get; set; }
    }
}
