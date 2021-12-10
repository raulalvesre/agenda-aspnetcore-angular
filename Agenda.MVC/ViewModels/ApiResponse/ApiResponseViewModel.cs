using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Agenda.MVC.ViewModels.ApiResponse
{
    public class ApiResponseViewModel<T>
    {

        public T Result { get; set; }
        public IEnumerable<ErrorViewModel> Errors { get; set; } = new List<ErrorViewModel>();
        public bool HasError { get => Errors.Any(); }
        public void AddErrorsToModelState(ModelStateDictionary modelState)
        {
            foreach (var error in Errors)
                modelState.AddModelError(string.Empty, error.ErrorMessage);
        }

    }
}
