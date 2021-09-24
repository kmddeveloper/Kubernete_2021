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
    [ErrorExceptionFilter]
    public class RegistrationController : BaseController
    {
        readonly IRegistrationService _registrationService;
        readonly IUserService _userService;        
        
        public RegistrationController(IRegistrationService registrationService, IUserService userService, ILogger<RegistrationController> logger ) :base(logger)
        {
            _registrationService = registrationService;
            _userService = userService;
        }

        [Route("get")]
        [HttpGet("{id}")]
        public IActionResult TestGet(int Id)
        {

            return Ok(Id);
        }

        [Route("test2")]
        [HttpPost]
        public IActionResult test2([FromBody] string test)
        {
          

            return Ok(test);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existUser = await _userService.GetUserByEmailAsync(user.Email);

                if (existUser != null)
                    throw new Exception($"User with email, {user.Email} is taken. Please try another email address!");

                var newUser = await _registrationService.RegisterUserAsync(user);

                if (newUser == null)
                    throw new Exception("Unable to register user!");

                return Ok(newUser);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
