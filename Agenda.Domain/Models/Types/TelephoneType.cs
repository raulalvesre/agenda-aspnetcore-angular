using Agenda.Domain.Core;

namespace Agenda.Domain.Models.Types
{
    public class TelephoneType : Enumeration
    {

        public static TelephoneType Landline = new TelephoneType(1, "Landline");
        public static TelephoneType Commercial = new TelephoneType(2, "Commercial");
        public static TelephoneType Cellphone = new TelephoneType(3, "Cellphone");

        public TelephoneType(int id, string name) : base(id, name) { }

    }

}
