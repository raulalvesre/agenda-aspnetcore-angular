using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Agenda.MVC.ViewModels.ContactTelephone;

namespace Agenda.MVC.ViewModels.Contact
{
    public class ContactFormViewModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "O nome não pode estar vazio")]
        [DisplayName("Nome")]
        public string Name { get; set; }

        [DisplayName("Dono")]
        public string OwnerId { get; set; }

        [Required]
        [DisplayName("Telefones")]
        public List<ContactTelephoneFormViewModel> Telephones { get; set; } = new List<ContactTelephoneFormViewModel>();

    }
}
