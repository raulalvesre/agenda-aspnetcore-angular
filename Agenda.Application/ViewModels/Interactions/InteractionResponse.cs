using Agenda.Application.ViewModels.User;

namespace Agenda.Application.ViewModels.Interactions
{
    public class InteractionResponse : RecordViewModel
    {

        public UserResponse WhoInteracted { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }

    }
}
