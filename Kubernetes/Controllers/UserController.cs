using EFModel;
using Kubernetes.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Filters;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ErrorExceptionFilter]
    public class UserController : BaseController
    {
        readonly IUserService _userService;
        readonly ITokenService _tokenService;
       
 
        public UserController(IUserService userService, ITokenService tokenService,  ILogger<UserController> logger):base(logger)
        {
            _userService = userService;
            _tokenService = tokenService;
            
        }

        // GET: api/User
        [Route("get")]
        [HttpGet("{id}")]        
        [Authorize]        
        public async Task<ActionResult> GetAsync(int Id)
        {
            var user = await _userService.GetUserByIdAsync(Id);
           
            return Ok(user);
        }

        




        [Authorize(Policy = "hasReportAccess")]
        [HttpGet("claim")]
        public ActionResult Claims()
        {
            var user= User.Claims.Select(c =>
                new
                {
                    Type = c.Type,
                    Value = c.Value
                });

            return Ok(user);
        }

        [HttpPost("GetUserPost")]
        public ActionResult GetUser([FromBody] User user)
        {
            _logger.LogWarning($"{DateTime.Now} --  Executing user controller");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }        
           return Ok(user);
          
        }

        private async Task GetMessages(HttpContext context, WebSocket webSocket)
        {
            var messages = new[]
            {
            "Message1",
            "Message2",
            "Message3",
            "Message4",
            "Message5",
            "Message6",
            "Message7",
            "Message8",
            "Message9",
            "Message10",
            "Message11",
            "Message12",
            "Message13",
        };

            foreach (var message in messages)
            {
                var bytes = Encoding.ASCII.GetBytes(message);
                var arraySegment = new ArraySegment<byte>(bytes);
                await webSocket.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
                Thread.Sleep(2000); //sleeping so that we can see several messages are sent
            }

            await webSocket.SendAsync(new ArraySegment<byte>(Encoding.ASCII.GetBytes("DONE")), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
