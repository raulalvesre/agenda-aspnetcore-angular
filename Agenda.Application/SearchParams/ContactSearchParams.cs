using System.Linq;
using Agenda.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Application.ViewModels
{
    public class ContactSearchParams : PaginationFilterParamsBase<Domain.Models.Contact>
    {

        public int? IdUsuario { get; set; }
        public int? IdContato { get; set; }
        public string NomeContato { get; set; }
        public int? Ddd { get; set; }
        public string NumeroTelefone { get; set; }

        protected override void Filter()
        {
            PreQuery(c => c.Include(c => c.Owner).ThenInclude(u => u.Role)
                            .Include(c => c.Telephones).ThenInclude(t => t.TelephoneType));

            if (IdUsuario.HasValue)
                And(c => c.OwnerId == IdUsuario);

            if (IdContato.HasValue)
                And(c => c.Id == IdContato);

            if (!string.IsNullOrEmpty(NomeContato))
                And(c => EF.Functions.Like(c.Name, $"%{NomeContato}%"));

            if (!string.IsNullOrEmpty(NumeroTelefone))
                And(c => c.Telephones.Any(c => EF.Functions.Like(c.TelephoneFormatted, $"%{NumeroTelefone}%")));

            if (Ddd.HasValue)
                And(c => c.Telephones.Any(c => c.Ddd == Ddd));
        }

    }
}
