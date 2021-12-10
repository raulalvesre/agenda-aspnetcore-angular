using System.Collections.Generic;
using System.Threading.Tasks;
using Agenda.Application.ViewModels;
using Agenda.Application.ViewModels.Interactions;
using Agenda.Domain.Core;

namespace Agenda.Application.Interfaces
{
    public interface IInteractionService
    {

        IEnumerable<EnumerationResponse> GetAllInteractionTypes();
        Task<Pagination<InteractionResponse>> Search(InteractionSearchParams searchQuery);

    }
}
