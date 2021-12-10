using System.Collections.Generic;
using System.Threading.Tasks;
using Agenda.Application.ViewModels;
using Agenda.Application.ViewModels.Contact;
using Agenda.Domain.Core;

namespace Agenda.Application.Interfaces
{
    public interface IPhonebookService
    {

        Task<ContactResponse> AddContact(ContactRequest contactVm);
        Task<ContactResponse> GetContactResponse(int id);
        Task<Pagination<ContactResponse>> Search(ContactSearchParams searchQuery);
        Task<ContactResponse> UpdateContact(int contactId, ContactRequest updatedContactVm);
        Task RemoveContact(int contactId);
        IEnumerable<EnumerationResponse> GetAllContactTelephoneTypes();
        Task<bool> IsPhoneNumberAlreadySavedInUserPhonebook(int userId, string telephoneNumber, int existingContactId = 0);
    }
}
