using System.Threading.Tasks;
using Agenda.Application.ViewModels.User;

namespace Agenda.Application.Interfaces
{
    public interface ILoginService
    {

        Task<UserResponse> Login(UserLoginRequest user);

    }
}
