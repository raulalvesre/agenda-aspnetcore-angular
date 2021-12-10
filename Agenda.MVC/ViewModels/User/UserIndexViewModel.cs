using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.MVC.ViewModels
{
    public class UserIndexViewModel
    {

        public IEnumerable<UserViewModel> Users { get; set; }
        public UserFormViewModel ModalForm { get; set; }

    }
}
