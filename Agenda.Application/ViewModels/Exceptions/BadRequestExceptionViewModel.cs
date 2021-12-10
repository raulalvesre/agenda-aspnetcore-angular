using System.Collections.Generic;
using Agenda.Application.ViewModels.Exceptions.Base;
using FluentValidation.Results;

namespace Agenda.Application.ViewModels.Exceptions
{

    public class BadRequestExceptionViewModel : ExceptionViewModel
    {

        public List<ValidationFailure> Errors { get; set; }

    }

}
