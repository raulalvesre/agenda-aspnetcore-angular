using Agenda.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Application.ViewModels.Interactions
{
    public class InteractionSearchParams : PaginationFilterParamsBase<Domain.Models.Interaction>
    {
        public int? InteractionId { get; set; }
        public int? TypeId { get; set; }
        public int? UserId { get; set; }
        public string Message { get; set; }

        protected override void Filter()
        {
            PreQuery(i => i.Include(i => i.InteractionType)
                            .Include(i => i.User).ThenInclude(u => u.Role));

            if (InteractionId.HasValue)
                And(i => i.Id == InteractionId);

            if (TypeId.HasValue)
                And(i => i.InteractionTypeId == TypeId);

            if (UserId.HasValue)
                And(i => i.UserId == UserId);

            if (!string.IsNullOrEmpty(Message))
                And(i => EF.Functions.Like(i.Message, $"%{Message}%"));
        }

    }
}
