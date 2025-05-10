using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NZWalks.CustomValidation;

public class ValidateAction:ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if(context.ModelState.IsValid == false)
        {
            context.Result = new BadRequestResult();
        }
    }
}
