using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Redarbor.Core.Exceptions;
using System.Net;

namespace Redarbor.Infrastructure.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            int statusCode;

            if (context.Exception.GetType() == typeof(BusinessException))
            {
                BusinessException exception = (BusinessException)context.Exception;
                var validationBusiness = new
                {
                    Detail = exception.Message
                };

                statusCode = (int)HttpStatusCode.BadRequest;

                var jsonBusiness = new
                {
                    Status = statusCode,
                    Title = "Bad Request",
                    errors = new[] { validationBusiness }
                };

                context.Result = new BadRequestObjectResult(jsonBusiness);
                context.HttpContext.Response.StatusCode = statusCode;
                context.ExceptionHandled = true;

                return;
            }

            statusCode = (int)HttpStatusCode.InternalServerError;

            var json = new
            {
                Status = statusCode,
                Title = "Internal Server Error",
                errors = new[] { "Oops! Something is technically wrong. Please try later.", context.Exception.Message }
            };

            context.Result = new BadRequestObjectResult(json);
            context.HttpContext.Response.StatusCode = statusCode;
            context.ExceptionHandled = true;
        }
    }
}
