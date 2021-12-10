using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Agenda.MVC.ViewModels.ContactTelephone;

namespace Agenda.MVC.ViewModels.Contact
{
    public class ContactViewModel : RecordViewModel
    {

        [Required]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [DisplayName("Dono")]
        public UserViewModel Owner { get; set; }

        [Required]
        [DisplayName("Telefones")]
        public List<ContactTelephoneViewModel> Telephones { get; set; } = new List<ContactTelephoneViewModel>();

    }
}
