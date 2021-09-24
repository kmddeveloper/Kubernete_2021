using EFModel;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class ErrorController : BaseController
    {
        public ErrorController(ILogger<ErrorController> logger) : base(logger) { }
        
        public ApiResponse<object> Index()
        {

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            var exception = context?.Error;

            var statusCode = HttpContext.Response.StatusCode;

            Response.StatusCode = statusCode;

            return new ApiResponse<object>
            {
                StatusCode = statusCode,
                ApiError = exception == null ? null : new Error(exception),
                Result = null
            };

            
        }        
    }
}
