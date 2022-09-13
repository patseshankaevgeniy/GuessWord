using GuessWord.Application.Common.Exceptions;
using GuessWord.Application.Common.Models;
using GuessWord.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using AccessViolationException = GuessWord.Application.Exceptions.AccessViolationException;

namespace GuessWord.Api.Infrastructure.Meddleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var result = new ApiErrorDto();

                switch (ex)
                {
                    case ValidationException validationException:
                        result.StatusCode = (int)HttpStatusCode.BadRequest;
                        result.Message = validationException.Message;
                        break;

                    case NotFoundException notFoundException:
                        result.StatusCode = (int)HttpStatusCode.NotFound;
                        result.Message = notFoundException.Message;
                        break;

                    case AccessViolationException accessViolationException:
                        result.StatusCode = (int)HttpStatusCode.Forbidden;
                        result.Message = accessViolationException.Message;
                        break;

                    case UnauthorizedException unauthorizedException:
                        result.StatusCode = (int)HttpStatusCode.Unauthorized;
                        result.Message = unauthorizedException.Message;
                        break;

                    default:
                        result.StatusCode = (int)HttpStatusCode.InternalServerError;
                        result.Message = ex.Message;
                        break;
                }

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = result.StatusCode;
                await context.Response.WriteAsJsonAsync(result);
            }
        }
    }
}
