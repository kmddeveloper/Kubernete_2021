using EFModel;
using Kubernetes.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Web.Filters;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ErrorExceptionFilter]
    public class AuthController : BaseController
    {
        readonly IAuthService _authService;
                
        public AuthController(IAuthService authService, ILogger<AuthController> logger):base(logger)
        {
            _authService = authService;            
        }

        [HttpPost] 
        [Consumes("application/json")]
        public async Task<IActionResult> Post([FromBody] UserCredentials userCredentials )
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Invalid Email or Password!");

                var userSession = await _authService.LoginAsync(userCredentials.Email, userCredentials.Password);


                return Ok(userSession);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       
    }
}
