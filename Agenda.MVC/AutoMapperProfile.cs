using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.MVC.ViewModels;
using Agenda.MVC.ViewModels.Contact;
using Agenda.MVC.ViewModels.ContactTelephone;
using AutoMapper;

namespace Agenda.MVC
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<UserViewModel, UserFormViewModel>();

            CreateMap<ContactViewModel, ContactFormViewModel>();

            CreateMap<ContactTelephoneViewModel, ContactTelephoneFormViewModel>()
                .ForMember(ctf => ctf.TelephoneNumber, ctx => ctx.MapFrom(ct => ct.TelephoneFormatted))
                .ForMember(ctf => ctf.Type, ctx => ctx.MapFrom(ct => ct.TypeId));

        }
    }
}
