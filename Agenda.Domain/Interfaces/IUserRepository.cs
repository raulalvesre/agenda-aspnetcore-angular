using System.Collections.Generic;
using System.Threading.Tasks;
using Agenda.Domain.Core;
using Agenda.Domain.Interfaces.Base;
using Agenda.Domain.Models;

namespace Agenda.Domain.Interfaces
{
    public interface IUserRepository : IRecordRepository<User>
    {
    }
}
