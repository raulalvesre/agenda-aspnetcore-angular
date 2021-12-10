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
using Agenda.Domain.Models.Types;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Application.Services
{
    public class PhonebookService : ContactServiceBase, IPhonebookService
    {

        public PhonebookService(IUser appUser,
                                IContactRepository contacts,
                                IInteractionRepository interactionRepository,
                                IUnitOfWork unitOfWork,
                                IMapper mapper) : base(contacts, interactionRepository, mapper, unitOfWork, appUser)
        {
        }

        public async Task<ContactResponse> AddContact(ContactRequest newContact)
        {
            var validate = await newContact.ValidateAsync();
            if (!validate.IsValid)
                throw new BadRequestException(validate.Errors);

            var telephoneConflicts = await GetTelephoneConflicts(AppUserId, newContact.Telephones.Select(t => t.TelephoneNumber));
            if (telephoneConflicts.Any())
                throw new ConflictException(telephoneConflicts);

            var contact = _mapper.Map<Contact>(newContact);
            contact.OwnerId = AppUserId;

            await _contactRepository.Add(contact);
            await AddContactAddedInteraction(newContact.Name);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<ContactResponse>(contact);
        }

        public async Task<ContactResponse> GetContactResponse(int id)
        {
            var dbContact = await GetContactModel(id);

            if (dbContact == null)
                throw new NotFoundException($"Nenhum contato com o ID = {id}");

            return _mapper.Map<ContactResponse>(dbContact);
        }

        public async Task<ContactResponse> UpdateContact(int contactId, ContactRequest updatedContact)
        {
            var validate = await updatedContact.ValidateAsync();
            if (!validate.IsValid)
                throw new BadRequestException(validate.Errors);

            var telephoneConflicts = await GetTelephoneConflicts(AppUserId, updatedContact.Telephones.Select(t => t.TelephoneNumber), contactId);
            if (telephoneConflicts.Any())
                throw new ConflictException(telephoneConflicts);

            var dbContact = await GetContactModel(contactId);
            if (dbContact == null)
                throw new NotFoundException($"Nenhum contato com o ID = {contactId}");

            if (dbContact.OwnerId != AppUserId)
                throw new ForbiddenException("Você não pode atualizar esse contato");

            updatedContact.Telephones.ForEach(t => t.ContactId = contactId);

            _mapper.Map(updatedContact, dbContact);

            await _contactRepository.Update(dbContact);
            await AddContactUpdatedInteraction(contactId, updatedContact.Name);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<ContactResponse>(dbContact);
        }

        public async Task RemoveContact(int contactId)
        {
            var dbContact = await GetContactModel(contactId);

            if (dbContact == null)
                throw new NotFoundException($"Nenhum contato com o ID = {contactId}");

            if (dbContact.OwnerId != AppUserId)
                throw new ForbiddenException("Você não pode remover esse contato");

            await _contactRepository.Remove(dbContact);
            await AddContactRemovedInteraction(contactId, dbContact.Name);
            await _unitOfWork.SaveAsync();
        }

        public async Task<Pagination<ContactResponse>> Search(ContactSearchParams sQ)
        {
            sQ.IdUsuario = AppUserId;
            var contatos = await _contactRepository.Paginate(sQ);

            return _mapper.Map<Pagination<ContactResponse>>(contatos);
        }

    }
}
