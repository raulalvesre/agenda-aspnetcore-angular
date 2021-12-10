using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.MVC.ViewModels.Contact;

namespace Agenda.MVC.ViewModels
{
    public class PhonebookViewModel
    {

        public PhonebookSearchViewModel SearchParameters { get; set; }
        public IEnumerable<ContactViewModel> Contacts { get; set; }

    }
}
