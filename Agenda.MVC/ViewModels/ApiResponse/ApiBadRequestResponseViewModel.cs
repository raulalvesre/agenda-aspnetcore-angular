using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.MVC.ViewModels.ApiResponse
{
    public class ApiBadRequestResponseViewModel
    {

        public string Message { get; set; }
        public IEnumerable<ErrorViewModel> Errors { get; set; }

    }
}
