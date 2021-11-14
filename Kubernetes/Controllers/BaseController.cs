using EFModel;
using GlobalConstants;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Filters;

namespace Web.Controllers
{
    [LogActionFilter]
    [ErrorExceptionFilter]
    public class BaseController : Controller
    {
        protected ILogger _logger;

        UserSession  _userSession = null;
        public BaseController(ILogger logger)
        {
            _logger = logger;            
        }

        protected UserSession SessionContext
        {
            get
            {
                if (_userSession == null || _userSession.UserId == 0)
                {
                    if (HttpContext != null && HttpContext.Items != null &&
                        HttpContext.Items.ContainsKey(GLOBAL_USER.USER_SESSION) &&
                        HttpContext.Items[GLOBAL_USER.USER_SESSION] != null &&
                        HttpContext.Items[GLOBAL_USER.USER_SESSION] is UserSession)
                    {
                        _userSession = HttpContext.Items[GLOBAL_USER.USER_SESSION] as UserSession;
                    }
                }
                return _userSession;
            }
        }


        protected ApiResponse<T> ApiResult<T>(T result) 
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
          
            var exception = context?.Error;

            var statusCode = HttpContext.Response.StatusCode;

            Response.StatusCode = statusCode;

            return new ApiResponse<T>
            {
                StatusCode = statusCode,
                ApiError = exception==null?null:new Error(exception),
                Result = result
            };
        }        
    }
}
