using System.Net;
using Microsoft.AspNetCore.Mvc;
using Shared.Error_Models;

namespace E_Commerce.Factories
{
    #region Part 4 Validation Error Handling 
    public class ApiResponseFactory
    {
        //Context ==> ModelState --> Dictionary <string, ModelEntry>
        //string ==> key , Name of Parameter 
        // ModelStateDictionary ==> Objects , Errors
        public static IActionResult CustomValidationErrorResponse(ActionContext context)
        {
            // 1 . Get All Errors in ModelState

            var errors = context.ModelState.Where(error => error.Value.Errors.Any())
                        .Select(error => new ValidationError
                        {
                            Field = error.Key,
                            Errors = error.Value.Errors.Select(e => e.ErrorMessage)
                        });
            // 2 . Create Custom Response

            var response = new ValidationErrorResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                ErrorMessage = "Validation Failed",
                Errors = errors

            };

            // 3 . return 
            return new BadRequestObjectResult(response);





        }
    } 
    #endregion
}
