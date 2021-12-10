using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace Agenda.Application.Exceptions
{
    public class BadRequestException : Exception
    {

        //essa eu copiei

        public List<ValidationFailure> Errors { get; set; } = new List<ValidationFailure>();

        public BadRequestException(List<ValidationFailure> errors) : this()
        {
            Errors = errors;
        }

        public BadRequestException() : base("Requisição inválida por favor verifique os dados e tente novamente.")
        {
        }

    }
}
