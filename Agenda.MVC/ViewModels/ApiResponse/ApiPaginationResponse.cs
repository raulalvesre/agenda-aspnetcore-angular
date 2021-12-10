using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.MVC.ViewModels.ApiResponse
{
    public class ApiPaginationResponse<T>
    {

        public int Skip { get; set; }
        public int Take { get; set; }
        public int Total { get; set; }
        public T Data { get; set; }

        public ApiPaginationResponse(int take, int skip, int total, T data)
        {
            Skip = skip;
            Take = take;
            Total = total;
            Data = data;
        }

    }
}
