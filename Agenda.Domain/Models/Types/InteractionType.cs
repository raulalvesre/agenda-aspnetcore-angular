using Agenda.Domain.Core;

namespace Agenda.Domain.Models.Types
{
    public class InteractionType : Enumeration
    {

        public static InteractionType CreateContact = new InteractionType(1, "Create Contact");
        public static InteractionType UpdateContact = new InteractionType(2, "Update Contact");
        public static InteractionType RemoveContact = new InteractionType(3, "Remove Contact");

        public static InteractionType CreateUser = new InteractionType(5, "Create User");
        public static InteractionType UpdateUser = new InteractionType(6, "Update User");
        public static InteractionType RemoveUser = new InteractionType(7, "Remove User");

        public static InteractionType UserLogin = new InteractionType(9, "User Login");

        public InteractionType(int id, string name) : base(id, name) { }

    }
}
