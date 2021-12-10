using Agenda.Domain.Core;

namespace Agenda.Domain.Models.Types
{
    public class UserRole : Enumeration
    {
        public static UserRole CreateContact = new UserRole(1, "ADMIN");
        public static UserRole UpdateContact = new UserRole(2, "STANDARD USER");

        public UserRole(int id, string name) : base(id, name) { }

    }
}
