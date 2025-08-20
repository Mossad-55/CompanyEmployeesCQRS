using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;

namespace CompanyEmployeesCQRS.Presentation.ActionFilters;

public class ValidateMediaTypeAttribute : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Getting the Accept attribute from the request header.
        var acceptHeaderPresent = context.HttpContext
            .Request.Headers.ContainsKey("Accept");

        // Check if it exists or not.
        if (!acceptHeaderPresent)
        {
            context.Result = new BadRequestObjectResult($"Accept header is missing.");
            return;
        }

        // Getting the Media Type from the Accept attribute.
        var mediaType = context.HttpContext
            .Request.Headers["Accept"].FirstOrDefault();

        // Check if we can parse the media type and if it's valid.
        if(!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue? outMediaType))
        {
            context.Result = new BadRequestObjectResult($"Media type not present. Please Add Accept header with the required media type.");
            return;
        }

        // Adding the Media Type to the AcceptHeaderMediaType key or Attribute.
        context.HttpContext.Items.Add("AcceptHeaderMediaType", outMediaType);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {

    }
}
