using System.Collections.Generic;
using System.Threading.Tasks;
using Agenda.Application.Interfaces;
using Agenda.Application.Services.Base;
using Agenda.Application.ViewModels;
using Agenda.Application.ViewModels.Interactions;
using Agenda.Domain.Core;
using Agenda.Domain.Interfaces;
using Agenda.Domain.Models.Types;
using AutoMapper;

namespace Agenda.Application.Services
{
    public class InteractionService : ServiceBase, IInteractionService
    {

        public InteractionService(IInteractionRepository interactionRepository,
            IUser appUser,
            IUnitOfWork unitOfWork,
            IMapper mapper) : base(interactionRepository, mapper, appUser, unitOfWork)
        {
        }

        public IEnumerable<EnumerationResponse> GetAllInteractionTypes()
        {
            return _mapper.Map<IEnumerable<EnumerationResponse>>(Enumeration.GetAll<InteractionType>());
        }

        public async Task<Pagination<InteractionResponse>> Search(InteractionSearchParams sQ)
        {
            var interactions = await _interactionRepository.Paginate(sQ);

            return _mapper.Map<Pagination<InteractionResponse>>(interactions);
        }
    }
}
