using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Agenda.MVC.ViewModels
{
    public class UserViewModel : RecordViewModel
    {

        [DisplayName("Nome")]
        public string Name { get; set; }

        public string Role { get; set; }
        public int RoleId { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

    }
}
