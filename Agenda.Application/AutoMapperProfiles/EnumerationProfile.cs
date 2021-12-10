using Agenda.Application.ViewModels;
using Agenda.Domain.Core;
using AutoMapper;

namespace Agenda.Application.AutoMapperProfiles
{
    public class EnumerationProfile : Profile
    {
        public EnumerationProfile()
        {
            CreateMap<Enumeration, EnumerationResponse>();
        }

    }
}
