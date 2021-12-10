using Agenda.Domain.Interfaces;
using Agenda.Domain.Models;
using Agenda.Domain.Core;
using Agenda.Domain.Interfaces.Base;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Agenda.Infrastructure.Repositories.Base
{
    public abstract class RecordRepository<T> : IRecordRepository<T> where T : Record
    {

        protected readonly ApplicationContext _database;

        public RecordRepository(ApplicationContext database)
        {
            _database = database;
        }

        public async Task Add(T record)
        {
            await _database.Set<T>().AddAsync(record);
        }

        public IQueryable<T> Query()
        {
            return _database.Set<T>().AsQueryable();
        }

        public Task Remove(T record)
        {
            _database.Set<T>().Remove(record);
            return Task.CompletedTask;
        }

        public Task Update(T record)
        {
            _database.Set<T>().Update(record);
            return Task.CompletedTask;
        }

        public async Task<Pagination<T>> Paginate(IPaginationFilterParams<T> filterParams)
        {
            var recordList = new List<T>();
            var query = filterParams.ApplyFilter(_database.Set<T>());
            int count = await query.CountAsync();
            int realSkip = filterParams.Skip.HasValue || filterParams.Skip > 0 ? (int)filterParams.Skip : 0;
            int realTake = filterParams.Take.HasValue ? (int)filterParams.Take : count;
            
            if (realTake < 1)
                return new Pagination<T>(realSkip, 0, count, recordList);

            recordList = await query
                .OrderBy(r => r.Id)
                .Skip(realSkip)
                .Take(realTake)
                .ToListAsync();

            return new Pagination<T>(realSkip, realTake, count, recordList);
        }

    }
}
