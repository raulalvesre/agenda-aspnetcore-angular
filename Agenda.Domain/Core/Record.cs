using System;

namespace Agenda.Domain.Core
{
    public abstract class Record
    {

        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }

    }
}
