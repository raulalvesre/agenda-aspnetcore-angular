using Agenda.Domain.Core;
using Agenda.Domain.Models.Types;


namespace Agenda.Domain.Models
{
    public class Interaction : Record
    {

        public string Message { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public int InteractionTypeId { get; set; }
        public InteractionType InteractionType { get; set; }

    }
}
