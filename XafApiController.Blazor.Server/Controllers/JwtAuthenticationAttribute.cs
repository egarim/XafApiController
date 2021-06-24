using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace XafApiController.Blazor.Server.Controllers
{
    public class JwtAuthenticationAttribute : ActionFilterAttribute
    {

        public JwtAuthenticationAttribute()
        {

        }
        //TODO implement protection https://github.com/cuongle/Hmac.WebApi/blob/master/Hmac.Api/Filters/AuthenticateAttribute.cs
        //TODO implement protection https://stackoverflow.com/questions/11775594/how-to-secure-an-asp-net-web-api
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            //context.Result = new UnauthorizedResult();

        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // do something after the action executes
        }
    }
}
