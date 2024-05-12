using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;

namespace HubtelWallet.API.Middlewares
{
    public class ValidationActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {

                context.Result = new BadRequestObjectResult(new
                {
                    status = false,
                    message = "One or more validation errors occurred. Find details in 'errors' property",
                    errors = ConvertErrors(context.ModelState)
                });
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        private static IReadOnlyDictionary<string, string[]>? ConvertErrors(ModelStateDictionary errors)
        {
            var _errors = new Dictionary<string, string[]>();

            foreach (var property in errors.AsEnumerable())
            {
                var propertyName = property.Key;
                var propertyValue = property.Value;

                if (propertyValue.Errors.Any())
                {
                    var errorMessages = new List<string>();

                    foreach (var error in propertyValue.Errors)
                        errorMessages.Add(error.ErrorMessage);

                    _errors[propertyName] = errorMessages.ToArray();
                }
            }

            return _errors;
        }
    }
}
