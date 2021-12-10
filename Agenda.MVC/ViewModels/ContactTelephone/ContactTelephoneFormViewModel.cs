using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Agenda.MVC.ViewModels.ContactTelephone.ValidatorAttributes;

namespace Agenda.MVC.ViewModels.ContactTelephone
{
    public class ContactTelephoneFormViewModel
    {

        [DisplayName("Tipo")]
        public string Type { get; set; }

        [Required(ErrorMessage = "A descrição não pode estar vazia")]
        [DisplayName("Descrição")]
        public string Description { get; set; }

        [ContactTelephoneNumberValidator]
        [DisplayName("Número")]
        public string TelephoneNumber { get; set; }
    }
}
