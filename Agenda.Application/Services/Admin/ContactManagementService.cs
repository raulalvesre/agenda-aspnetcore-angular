using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Application.Exceptions;
using Agenda.Application.Interfaces;
using Agenda.Application.Services.Base;
using Agenda.Application.ViewModels;
using Agenda.Application.ViewModels.Contact;
using Agenda.Domain.Core;
using Agenda.Domain.Interfaces;
using Agenda.Domain.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Application.Services.Admin
{
    public class ContactManagementService : ContactServiceBase, IContactManagementService
    {

        private readonly IUserRepository _userRepository;

        public ContactManagementService(IContactRepository contactRepository,
            IUserRepository userRepository,
            IInteractionRepository interactionRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUser appUser) : base(contactRepository, interactionRepository, mapper, unitOfWork, appUser)
        {
            _userRepository = userRepository;
        }

        public async Task<ContactAdminResponse> Create(ContactAdminRequest newContact)
        {
            var validate = await newContact.ValidateAsync();
            if (!validate.IsValid)
                throw new BadRequestException(validate.Errors);

            var telephoneConflicts = await GetTelephoneConflicts(newContact.OwnerId, newContact.Telephones.Select(t => t.TelephoneNumber));
            if (telephoneConflicts.Any())
                throw new ConflictException(telephoneConflicts);

            var contactOwner = await GetUser(newContact.OwnerId);
            if (contactOwner == null)
                throw new NotFoundException($"Nenhum usuário com o ID = {newContact.OwnerId}");

            var contact = _mapper.Map<Contact>(newContact);

            await _contactRepository.Add(contact);
            await AddContactAddedInteraction(newContact.Name);
            await _unitOfWork.SaveAsync();

            contact.Owner = contactOwner;
            return _mapper.Map<ContactAdminResponse>(contact);
        }

        public async Task<ContactAdminResponse> GetContactAdminResponse(int id)
        {
            var contact = await GetContactModel(id);

            if (contact == null)
                throw new NotFoundException($"Nenhum contato com o ID = {id}");

            return _mapper.Map<ContactAdminResponse>(contact);
        }

        public async Task<Pagination<ContactAdminResponse>> Search(ContactSearchParams sQ)
        {
            var contatos = await _contactRepository.Paginate(sQ);

            return _mapper.Map<Pagination<ContactAdminResponse>>(contatos);
        }

        public async Task<ContactAdminResponse> Update(int contactId, ContactAdminRequest updatedContact)
        {
            var validate = await updatedContact.ValidateAsync();
            if (!validate.IsValid)
                throw new BadRequestException(validate.Errors);

            var telephoneConflicts = await GetTelephoneConflicts(updatedContact.OwnerId, updatedContact.Telephones.Select(t => t.TelephoneNumber), contactId);
            if (telephoneConflicts.Any())
                throw new ConflictException(telephoneConflicts);

            var dbContact = await GetContactModel(contactId);
            if (dbContact == null)
                throw new NotFoundException($"Nenhum contato com o ID = {contactId}");

            var contactOwner = await GetUser(updatedContact.OwnerId);
            if (contactOwner == null)
                throw new NotFoundException($"Nenhum usuário com o ID = {updatedContact.OwnerId}");

            updatedContact.Telephones.ForEach(t => t.ContactId = contactId);

            _mapper.Map(updatedContact, dbContact);

            await _contactRepository.Update(dbContact);
            await AddContactUpdatedInteraction(contactId, updatedContact.Name);
            await _unitOfWork.SaveAsync();

            dbContact.Owner = contactOwner;
            return _mapper.Map<ContactAdminResponse>(dbContact);
        }

        public async Task Delete(int contactId)
        {
            var dbContact = await GetContactModel(contactId);

            if (dbContact == null)
                throw new NotFoundException($"Nenhum contato com o ID = {contactId}");

            await _contactRepository.Remove(dbContact);
            await AddContactRemovedInteraction(contactId, dbContact.Name);
            await _unitOfWork.SaveAsync();
        }

        private async Task<User> GetUser(int userId)
        {
            return await _userRepository.Query()
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
