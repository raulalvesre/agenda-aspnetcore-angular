using System.Threading.Tasks;
using Agenda.Application.Interfaces;
using Agenda.Domain.Core;
using Agenda.Domain.Interfaces;
using Agenda.Domain.Models;
using AutoMapper;

namespace Agenda.Application.Services.Base
{
    public abstract class ServiceBase
    {
        protected readonly IInteractionRepository _interactionRepository;
        protected readonly IMapper _mapper;
        protected readonly IUser _appUser;
        protected readonly IUnitOfWork _unitOfWork;

        protected readonly int AppUserId;
        protected readonly string AppUserUsername;

        public ServiceBase(IInteractionRepository interactionRepository,
            IMapper mapper,
            IUser appUser,
            IUnitOfWork unitOfWork)
        {
            _interactionRepository = interactionRepository;
            _mapper = mapper;
            _appUser = appUser;
            _unitOfWork = unitOfWork;

            AppUserId = _appUser.GetUserId();
            AppUserUsername = _appUser.GetUsername();
        }

        protected async Task AddInteraction(int typeId, string message, int userId = 0)
        {
            if (userId == 0)
                userId = AppUserId;

            var interaction = new Interaction
            {
                UserId = userId,
                InteractionTypeId = typeId,
                Message = message
            };

            await _interactionRepository.Add(interaction);
        }

    }
}
