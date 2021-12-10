using System;

namespace Agenda.Application.ViewModels
{
    public class RecordViewModel
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }

    }
}
