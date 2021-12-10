using System.Linq;
using System.Threading.Tasks;
using Agenda.Domain.Core;

namespace Agenda.Domain.Interfaces.Base
{
    public interface IRecordRepository<T> where T : Record
    {

        Task Add(T record);
        Task Update(T record);
        Task Remove(T record);
        IQueryable<T> Query();
        Task<Pagination<T>> Paginate(IPaginationFilterParams<T> filterParams);

    }
}
