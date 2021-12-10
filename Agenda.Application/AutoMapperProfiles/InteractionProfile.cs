using System.Linq;
using Agenda.Application.ViewModels.Interactions;
using Agenda.Domain.Core;
using Agenda.Domain.Models;
using Agenda.Domain.Models.Types;
using AutoMapper;

namespace Agenda.Application.AutoMapperProfiles
{
    public class InteractionProfile : Profile
    {

        public InteractionProfile()
        {
            CreateMap<Interaction, InteractionResponse>()
                .ForMember(iR => iR.WhoInteracted, ctx => ctx.MapFrom(i => i.User))
                .ForMember(iR => iR.Type, ctx => ctx.MapFrom(i => i.InteractionType.Name));

            CreateMap(typeof(Pagination<>), typeof(Pagination<>));
        }

    }
}
