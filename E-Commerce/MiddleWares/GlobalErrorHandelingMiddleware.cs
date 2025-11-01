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
            // Set Content Typr ==> Apploication\json
            httpContext.Response.ContentType = "application/json";
            // Set Default Status code ==> 500 
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            // Return Standard Response
            httpContext.Response.StatusCode = ex switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound, //404
                _ => (int)HttpStatusCode.InternalServerError //500
            };

            var response = new ErrorDetails  // C# Object
            {
                StatusCode = httpContext.Response.StatusCode,
                ErrorMessage = ex.Message
            }.ToString();


            await httpContext.Response.WriteAsync(response);
        } 
        #endregion
    }
}
