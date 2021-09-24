using Kubernetes.Services;
using Kubernetes.TransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController :BaseController
    {
        readonly ITokenService _tokenService;
        

        public TokenController(ITokenService tokenService, ILogger<TokenController> logger ):base(logger)
        {
            _tokenService = tokenService;            
        }

        [HttpPost("create")]
        public async Task<JsonWebToken> CreateAsync([FromBody] string sessionId)
        {
            return await _tokenService.CreateJWTAsync(sessionId);            
        }
    }
}
