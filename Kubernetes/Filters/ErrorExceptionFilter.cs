using EFModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;



namespace Web.Filters
{
    
    public class ErrorExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var conntroller = context.RouteData.Values["controller"];
            var actionName = context.RouteData.Values["action"];
            var value = context;


            

            //context.Result = new JsonResult(new ApiResponse<object> {Error=error, Result=null })
            //{
            //    StatusCode = (int)HttpStatusCode.InternalServerError
            //};


            base.OnException(context);
        }
    }

}
