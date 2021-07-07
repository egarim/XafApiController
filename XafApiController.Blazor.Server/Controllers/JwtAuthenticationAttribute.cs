using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Diagnostics;
using System.Linq;
using XafApiController.Blazor.Server.Services;

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

            ////Todo read if authentication is on
            //// do something before the action executes
            //var AuthId = context.HttpContext.Request.Headers["AuthId"];
            var Token = context.HttpContext.Request.Headers["Authorization"];

            var JwtService = context.HttpContext.RequestServices.GetService(typeof(IJwtService)) as IJwtService;


            try
            {

                var Payload = JwtService.TokenToJwtPayload(Token);
                Debug.WriteLine(Payload);


                if (!JwtService.VerifyToken(Token))
                {
                    context.Result = new UnauthorizedResult();
                }
            }
            catch (System.Exception)
            {

              
                context.Result = new UnauthorizedResult();
            };


        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // do something after the action executes
        }
    }
}
