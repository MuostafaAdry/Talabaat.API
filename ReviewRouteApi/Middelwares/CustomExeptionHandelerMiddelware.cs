using Domain.Exceptions;
using Shared.ErrorModels;
using System.Threading.Tasks;

namespace ReviewRouteApi.Middelwares
{
    public class CustomExeptionHandelerMiddelware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public CustomExeptionHandelerMiddelware(RequestDelegate next, ILogger<CustomExeptionHandelerMiddelware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);

                // if end post not found
                if (httpContext.Response.StatusCode==StatusCodes.Status404NotFound)
                {
                    var Responce = new ErrorToReturn()
                    {
                        StatusCode=StatusCodes.Status404NotFound,
                        ErrorMessage=$"This End Point {httpContext.Request.Path} Not Found"
                    };
                    // convert object to json
                   await httpContext.Response.WriteAsJsonAsync(Responce);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Happened ");
                // set status code
                //set object
                var ResponceBadRequest = new ErrorToReturn()
                {
                    // dynamic status code 
                    ErrorMessage=ex.Message
                };
                httpContext.Response.StatusCode = ex switch
                {
                    BadRequestException BadRequestException => GetBadRequestException(BadRequestException, ResponceBadRequest),
                    UnauthorizedException => StatusCodes.Status401Unauthorized,
                    BaseNotFountException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };

                var Responce = new ErrorToReturn()
                {
                    // dynamic status code 
                    StatusCode = httpContext.Response.StatusCode,
                    ErrorMessage = ex.Message
                };
                // return object as json
                await httpContext.Response.WriteAsJsonAsync(Responce);
            }

        }

        private int GetBadRequestException(BadRequestException badRequestException, ErrorToReturn responce)
        {
            responce.StatusCode =StatusCodes.Status400BadRequest;
            responce.Errors = badRequestException.Errors;
            return StatusCodes.Status400BadRequest;
        }
    }
}
