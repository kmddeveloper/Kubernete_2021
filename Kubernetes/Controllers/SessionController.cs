using EFModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;



namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : BaseController
    {
        public SessionController(ILogger<SessionController> logger) : base(logger) { }

        [Authorize]    
        [HttpGet(Name ="GetContext")]
        [Route("GetContext")]
        public ApiResponse<UserSession> GetSessionContext()
        {           
            if (SessionContext == null)
                throw new Exception("Invalid Bearer Token!");

            return ApiResult<UserSession>(SessionContext);
          
        }
    }
}
