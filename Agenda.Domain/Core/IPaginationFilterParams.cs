using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Domain.Core
{
    public interface IPaginationFilterParams<T> where T : Record
    {

        int? Skip { get; set; }
        int? Take { get; set; }
        IQueryable<T> ApplyFilter(IQueryable<T> query);

    }
}
