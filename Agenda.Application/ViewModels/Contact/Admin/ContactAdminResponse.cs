using Agenda.Application.ViewModels.Contact.Base;
using Agenda.Application.ViewModels.User;

namespace Agenda.Application.ViewModels.Contact
{
    public class ContactAdminResponse : ContactResponseBase
    {
        public UserResponse Owner { get; set; }
    }
}
