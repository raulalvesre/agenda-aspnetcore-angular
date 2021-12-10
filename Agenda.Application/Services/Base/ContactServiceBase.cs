using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Application.ViewModels;
using Agenda.Domain.Core;
using Agenda.Domain.Interfaces;
using Agenda.Domain.Models;
using Agenda.Domain.Models.Types;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Application.Services.Base
{
    public abstract class ContactServiceBase : ServiceBase
    {

        protected readonly IContactRepository _contactRepository;

        public ContactServiceBase(IContactRepository contactRepository,
            IInteractionRepository interactionRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUser appUser) : base(interactionRepository, mapper, appUser, unitOfWork)
        {
            _contactRepository = contactRepository;
        }

        public IEnumerable<EnumerationResponse> GetAllContactTelephoneTypes()
        {
            var types = Enumeration.GetAll<TelephoneType>();

            return _mapper.Map<IEnumerable<EnumerationResponse>>(types);
        }

        protected async Task<List<string>> GetTelephoneConflicts(int userId, IEnumerable<string> telephoneNumbers, int existingContactId = 0)
        {
            var telephoneConflicts = new List<string>();

            foreach (var tel in telephoneNumbers)
            {
                var isTelephoneAlreadyRegisteredTask = IsPhoneNumberAlreadySavedInUserPhonebook(userId, tel, existingContactId);

                var isTelephoneAlreadyRegistered = await isTelephoneAlreadyRegisteredTask;
                if (isTelephoneAlreadyRegistered)
                    telephoneConflicts.Add($"O telefone {tel} já está registrado");
            }

            return telephoneConflicts;
        }

        public async Task<bool> IsPhoneNumberAlreadySavedInUserPhonebook(int userId, string telephoneNumber, int existingContactId = 0)
        {
            return await _contactRepository.Query()
                .Include(c => c.Telephones)
                .Where(c => c.OwnerId == userId)
                .Where(c => c.Id != existingContactId)
                .SelectMany(c => c.Telephones)
                .Select(t => t.TelephoneFormatted)
                .AnyAsync(tF => tF.Equals(telephoneNumber));
        }

        protected async Task<Contact> GetContactModel(int contactId)
        {
            return await _contactRepository.Query()
               .Include(c => c.Telephones).ThenInclude(t => t.TelephoneType)
               .Include(c => c.Owner)
               .FirstOrDefaultAsync(c => c.Id == contactId);
        }

        protected async Task AddContactAddedInteraction(string contactName)
        {
            await AddInteraction(InteractionType.CreateContact.Id, contactName + " was created");
        }

        protected async Task AddContactUpdatedInteraction(int contactId, string contactName)
        {
            await AddInteraction(InteractionType.UpdateContact.Id, $"{contactName}[{contactId}] was updated");

        }

        protected async Task AddContactRemovedInteraction(int contactId, string contactName)
        {
            await AddInteraction(InteractionType.RemoveContact.Id, $"{contactName}[{contactId}] was removed");
        }

    }
}
