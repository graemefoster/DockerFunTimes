using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DDDPerth
{
    public class ReturnValidationErrorsFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var asValidationIssue = context.Exception as ArgumentException;
            if (asValidationIssue != null) {
                context.Result = new Microsoft.AspNetCore.Mvc.BadRequestObjectResult(new ErrorResponse {
                    Message = "Validation errors",
                    Errors = asValidationIssue.Message
                });
                context.ExceptionHandled = true;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        //To support Xml data-contract serialization which doesn't work on anonymous types!
        //Not that we'd use Xml! It's more for demo purposes.
        public class ErrorResponse {
            public string Errors {get;set;}
            public string Message {get;set;}

        }
    }
}