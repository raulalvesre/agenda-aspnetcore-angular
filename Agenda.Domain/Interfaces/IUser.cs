using System.Collections.Generic;
using System.Security.Claims;

namespace Agenda.Domain.Interfaces
{
    public interface IUser
    {
        int GetUserId();
        string GetUsername();
        string GetUserEmail();
        string GetUserRole();
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
