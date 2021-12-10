using Agenda.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Application.ViewModels.User
{
    public class UserSearchParams : PaginationFilterParamsBase<Domain.Models.User>
    {

        public int? Id { get; set; }
        public int? RoleId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }

        protected override void Filter()
        {
            PreQuery(u => u.Include(u => u.Role));

            if (Id.HasValue)
                And(u => u.Id == Id);

            if (RoleId.HasValue)
                And(u => u.RoleId == RoleId);

            if (!string.IsNullOrEmpty(Name))
                And(u => EF.Functions.Like(u.Name, $"%{Name}%"));

            if (!string.IsNullOrEmpty(Email))
                And(u => EF.Functions.Like(u.Email, $"%{Email}%"));

            if (!string.IsNullOrEmpty(Username))
                And(u => EF.Functions.Like(u.Username, $"%{Username}%"));
        }
    }
}
