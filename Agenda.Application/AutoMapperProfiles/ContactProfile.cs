using Agenda.Application.Extensions;
using Agenda.Application.ViewModels.Contact;
using Agenda.Domain.Models;

using AutoMapper;

namespace Agenda.Application.AutoMapperProfiles
{
    public class ContactProfile : Profile
    {

        public ContactProfile()
        {
            CreateMap<ContactRequest, Contact>()
                .MergeList(m => m.Telephones, vm => vm.Telephones);

            CreateMap<ContactAdminRequest, Contact>()
                .MergeList(m => m.Telephones, vm => vm.Telephones);

            CreateMap<Contact, ContactResponse>();

            CreateMap<Contact, ContactAdminResponse>();
        }

    }
}
