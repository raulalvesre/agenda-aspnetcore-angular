using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.MVC.ViewModels
{
    public class InteractionViewModel : RecordViewModel
    {

        public string Type { get; set; }
        public string Message { get; set; }
        public UserViewModel WhoInteracted { get; set; }

    }
}
