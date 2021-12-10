using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Domain.Core
{
    public class Pagination<T>
    {

        public int Skip { get; set; }
        public int Take { get; set; }
        public int Total { get; set; }
        public IEnumerable<T> Data { get; set; }

        public Pagination(int skip, int take, int total, IEnumerable<T> data)
        {
            Skip = skip;
            Take = take;
            Total = total;
            Data = data;
        }

    }
}
