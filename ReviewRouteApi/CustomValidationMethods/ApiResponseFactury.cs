using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace ReviewRouteApi.CustomValidationMethods
{
    public class ApiResponseFactury
    {
       public static IActionResult GenirateApiValidationErrors(ActionContext context)
        {
            var Errors = context.ModelState.Where(e => e.Value.Errors.Any())
                  .Select(m => new ValidationError()
                  {
                      Filed = m.Key,
                      Error = m.Value.Errors.Select(e => e.ErrorMessage)
                  });


            var Response = new ValidationErrorToReturn()
            {

                ValidationErrors = Errors
            };
            return new BadRequestObjectResult(Response);
        }
    }
}
