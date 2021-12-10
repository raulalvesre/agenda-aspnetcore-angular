using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.Domain.Core;
using Agenda.Domain.Interfaces;
using Agenda.Domain.Models;
using Agenda.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Infrastructure.Repositories
{
    public class UserRepository : RecordRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext database) : base(database) { }

    }
}
