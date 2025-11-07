using System.Net;
using System.Text.Json;
using Domain.Exceptions;
using Shared.Error_Models;

namespace E_Commerce.MiddleWares
{
    #region Part 2 Global Exception Handling Middleware Server Error (Exception)
    public class GlobalErrorHandelingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandelingMiddleware> _logger;

        public GlobalErrorHandelingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandelingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        // Response [Status Code , Erorr Message ]
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
                if (httpContext.Response.StatusCode == (int )HttpStatusCode.NotFound)
                 await HandleNotFoundEndPointAsync(httpContext);

                
            }
            catch (Exception ex)
            {
                // Log Exception 
                _logger.LogError($"Something Went Wrong {ex}");
                // Handle Exception
                await HandleExceptionAsync(httpContext, ex);

            }


        }

        #region Part 5 Handling Not Found EndPoints
        private async Task HandleNotFoundEndPointAsync(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            var response = new ErrorDetails
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                ErrorMessage = $"The End Point {httpContext.Request.Path} Not Found"
            };

            await httpContext.Response.WriteAsJsonAsync(response);
        } 
        #endregion

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            // 2. Change Content Type ==> Apploication\json
            httpContext.Response.ContentType = "application/json";

            var response = new ErrorDetails  // C# Object --> Json Object
            {
                // StatusCode = httpContext.Response.StatusCode,
                ErrorMessage = ex.Message
            };


            // Change Status Code Based On Exception Type (500 & 400)
            httpContext.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound, //404
                UnAuthorizedException => StatusCodes.Status401Unauthorized, //401
                ValidationException validationException => HandleValidationException(validationException,response), //400
                _ => StatusCodes.Status500InternalServerError //500
            };
           
            response.StatusCode = httpContext.Response.StatusCode;


            await httpContext.Response.WriteAsync(response.ToString());
        }

        private int HandleValidationException(ValidationException validationException, ErrorDetails response)
        {
           response.Errors = validationException.Errors;
            return StatusCodes.Status400BadRequest;
        }
        #endregion
    }
}
