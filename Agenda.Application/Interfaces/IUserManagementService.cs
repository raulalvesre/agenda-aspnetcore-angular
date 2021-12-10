using System.Collections.Generic;
using System.Threading.Tasks;
using Agenda.Application.ViewModels;
using Agenda.Application.ViewModels.User;
using Agenda.Domain.Core;

namespace Agenda.Application.Interfaces
{
    public interface IUserManagementService
    {

        Task<UserResponse> CreateUser(UserRequest newUser);
        Task<UserResponse> GetUserResponse(int id);
        Task<Pagination<UserResponse>> Search(UserSearchParams searchQuery);
        Task<UserResponse> UpdateUser(int userId, UserRequest updatedUser);
        Task DeleteUser(int userId);
        IEnumerable<EnumerationResponse> GetAllUserTypes();
        Task<bool> IsUsernameAlreadyRegistered(string username, int existingUserId = 0);
        Task<bool> IsEmailAlreadyRegistered(string email, int existingUserId = 0);

    }
}
