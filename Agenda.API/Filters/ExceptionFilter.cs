using System.Net;
using Agenda.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Agenda.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is BadRequestException)
            {
                var exception = context.Exception as BadRequestException;
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new JsonResult(new
                {
                    Message = exception.Message,
                    Errors = exception.Errors
                });
            }

            if (context.Exception is ConflictException)
            {
                var exception = context.Exception as ConflictException;
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
                context.Result = new JsonResult(new
                {
                    Conflicts = exception.Conflicts
                });
            }

            if (context.Exception is ForbiddenException)
            {
                var exception = context.Exception as ForbiddenException;
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                context.Result = new JsonResult(new
                {
                    Message = exception.Message
                });
            }

            if (context.Exception is NotFoundException)
            {
                var exception = context.Exception as NotFoundException;
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Result = new JsonResult(new
                {
                    Message = exception.Message
                });
            }

            if (context.Exception is UnauthorizedException)
            {
                var exception = context.Exception as UnauthorizedException;
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Result = new JsonResult(new
                {
                    Message = exception.Message
                });
            }
        }
    }
}
