using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.MVC.ViewModels
{
    public abstract class RecordViewModel
    {

        [DisplayName("Id")]
        public int Id { get; set; }
        [DisplayName("Data de criação")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Data da última atualização")]
        public DateTime? LastUpdateDate { get; set; }

    }
}
