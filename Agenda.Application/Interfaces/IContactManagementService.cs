using System.Collections.Generic;
using System.Threading.Tasks;
using Agenda.Application.ViewModels;
using Agenda.Application.ViewModels.Contact;
using Agenda.Domain.Core;

namespace Agenda.Application.Interfaces
{
    public interface IContactManagementService
    {

        Task<ContactAdminResponse> Create(ContactAdminRequest contactVm);
        Task<ContactAdminResponse> GetContactAdminResponse(int id);
        Task<Pagination<ContactAdminResponse>> Search(ContactSearchParams searchQuery);
        Task<ContactAdminResponse> Update(int contactId, ContactAdminRequest updatedContactVm);
        Task Delete(int contactId);
        IEnumerable<EnumerationResponse> GetAllContactTelephoneTypes();
        Task<bool> IsPhoneNumberAlreadySavedInUserPhonebook(int userId, string telephoneNumber, int existingContactId = 0);


    }
}
