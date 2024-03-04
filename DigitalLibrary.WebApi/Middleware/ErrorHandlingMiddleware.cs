using DigitalLibrary.Infrastructure.Utilities;
using System.Net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLibrary.WebApi.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke (HttpContext context)
        {
            try
            {
                await _next (context);
            }
            catch (Exception error)
            {
                HttpResponse response = context.Response;
                response.ContentType = "application/json";
                switch (error)
                {
                    case AppException:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    case NotImplementedException:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotImplemented;
                        break;

                    case UnauthorizedAccessException:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;

                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                
                string message = JsonConvert.SerializeObject(new { message = error.Message });
                await response.WriteAsync(message);
            }
        }
    }
}
