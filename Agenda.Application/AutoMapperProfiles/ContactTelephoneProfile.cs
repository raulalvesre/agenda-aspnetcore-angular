using System.Linq;
using System.Text.RegularExpressions;
using Agenda.Application.ViewModels.ContactTelephone;
using Agenda.Domain.Core;
using Agenda.Domain.Models;
using Agenda.Domain.Models.Types;
using AutoMapper;
using AutoMapper.EquivalencyExpression;

namespace Agenda.Application.AutoMapperProfiles
{
    public class ContactTelephoneProfile : Profile
    {

        public ContactTelephoneProfile()
        {
            CreateMap<ContactTelephoneRequest, ContactTelephone>()
                .ForMember(t => t.TelephoneTypeId, ctx => ctx.MapFrom(tR => tR.Type))
                .ForMember(t => t.Ddd, ctx => ctx.MapFrom((tR, f) => int.Parse(Regex.Replace(tR.TelephoneNumber.Split(" ")[0], "[\\(\\)]", string.Empty))))
                .ForMember(t => t.TelephoneOnlyNumbers, ctx => ctx.MapFrom((tR, f) => tR.TelephoneNumber.Split(" ")[1].Replace("-", string.Empty)))
                .ForMember(t => t.TelephoneFormatted, ctx => ctx.MapFrom(tR => tR.TelephoneNumber));

            CreateMap<ContactTelephone, ContactTelephoneResponse>()
                .EqualityComparison((vm, model) => vm.Id == model.Id)
                .ForMember(tR => tR.Type, ctx => ctx.MapFrom(t => GetTelephoneTypeNameByItsId(t.TelephoneTypeId)));
        }

        private string GetTelephoneTypeNameByItsId(int telTypeId)
        {
            return Enumeration.GetAll<TelephoneType>().FirstOrDefault(tT => tT.Id == telTypeId).Name;
        }

    }
}
