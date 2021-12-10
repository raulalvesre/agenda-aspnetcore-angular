using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agenda.MVC.ViewModels;
using Agenda.MVC.ViewModels.ApiResponse;
using Flurl.Http;

namespace Agenda.MVC.ApiHttpServices
{
    public static class FlurlExtensions
    {
        public static async Task<ApiResponseViewModel<T>> GetApiResponse<T>(this Task<IFlurlResponse> task)
        {
            var response = await task;

            if (response.StatusCode == 400)
            {
                var result = await response.GetJsonAsync<ApiBadRequestResponseViewModel>();

                return new ApiResponseViewModel<T>
                {
                    Result = default(T),
                    Errors = result.Errors
                };
            }

            if (response.StatusCode == 409)
            {
                var result = await response.GetJsonAsync<ApiConflictResponseViewModel>();
                List<ErrorViewModel> errors = result.Conflicts.Select(c => new ErrorViewModel { ErrorMessage = c }).ToList();

                return new ApiResponseViewModel<T>
                {
                    Result = default(T),
                    Errors = errors
                };
            }

            return new ApiResponseViewModel<T>
            {
                Result = await response.GetJsonAsync<T>()
            };
        }
    }

}

