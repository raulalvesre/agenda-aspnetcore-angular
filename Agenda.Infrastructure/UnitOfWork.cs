using System.Threading.Tasks;
using Agenda.Domain.Core;

namespace Agenda.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationContext _database;

        public UnitOfWork(ApplicationContext database)
        {
            _database = database;
        }

        public async Task SaveAsync()
        {
            await _database.SaveChangesAsync();
        }

    }
}
