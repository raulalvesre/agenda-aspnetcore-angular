using System.Threading.Tasks;

namespace Agenda.Domain.Core
{
    public interface IUnitOfWork
    {

        Task SaveAsync();

    }
}
