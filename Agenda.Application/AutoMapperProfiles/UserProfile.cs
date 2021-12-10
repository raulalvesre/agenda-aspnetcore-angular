using System.Linq;
using Agenda.Application.ViewModels.User;
using Agenda.Domain.Core;
using Agenda.Domain.Models;
using Agenda.Domain.Models.Types;
using AutoMapper;

namespace Agenda.Application.AutoMapperProfiles
{
    public class UserProfile : Profile
    {

        public UserProfile()
        {
            CreateMap<UserRequest, User>()
                .ReverseMap();

            CreateMap<User, UserResponse>()
                .ForMember(uR => uR.Role, ctx => ctx.MapFrom(u => GetUserRoleNameByItsId(u.RoleId)));

            CreateMap(typeof(Pagination<>), typeof(Pagination<>));
        }

        private string GetUserRoleNameByItsId(int userRoleId)
        {
            return Enumeration.GetAll<UserRole>().FirstOrDefault(uR => uR.Id == userRoleId).Name;
        }

    }
}
