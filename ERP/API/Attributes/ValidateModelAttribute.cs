using ErrorCodeLib.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Linq;

namespace API.Attributes
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var result = context.ModelState.Keys
                      .SelectMany(key => context.ModelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                        .ToList();
                var resulModel = new ResultModel();
                resulModel = ErrorCodeLib.ErrorCode.PARAMETER_INVALID(result, null, "cht");
                context.Result = new ObjectResult(resulModel);

            }
            base.OnActionExecuting(context);
        }

        public class ValidationError
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string Field { get; }
            public string Message { get; }
            public ValidationError(string field, string message)
            {
                Field = field != string.Empty ? field : null;
                Message = message;
            }
        }

    }
}
