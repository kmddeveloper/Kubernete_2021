using EFModel;
using Kubernetes.TransferObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Kubernetes.Services
{
    public class TokenService : ITokenService
    {
        readonly ISessionService _sessionService;
        readonly IConfiguration _configuration;

        public TokenService(ISessionService sessionService, IConfiguration configuration)
        {
            _sessionService = sessionService;
            _configuration = configuration;
        }

        public async Task<JsonWebToken> CreateJWTAsync(string sessionId)
        {
            if (!String.IsNullOrEmpty(sessionId))
            {
                var userSession= await _sessionService.GetAsync(sessionId);
                return CreateJWT(userSession);
            }
            return null;
        }

        public JsonWebToken CreateJWT(UserSession userSession)
        {
            var now = DateTime.UtcNow;
            var claims = new Claim[]
            {
            new Claim(JwtRegisteredClaimNames.UserId, userSession.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.UserName, userSession.UserName),                       
            new Claim(JwtRegisteredClaimNames.SessionId, userSession.Id),
            new Claim("accesses", "report"),
            new Claim("roles", "test"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64),
            new Claim(ClaimTypes.Role, userSession.Role),
     
            new Claim(ClaimTypes.Name, "Kevin"),        
            new Claim("Kevin", "123")
            };

            var signingKey = GetSymmetricSecurityKey();

            if (signingKey == null)
                throw new ArgumentException("Unable to create SymetricSecurityKey!");

            var jwt = new JwtSecurityToken(
                issuer: _configuration["Token:Iss"],
                audience: _configuration["Token:Aud"],
                claims: claims,
               
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(String.Compare(userSession.Role, "guest", true)==0? Settings.Token.GUEST_TOKEN_EXPIRE_IN_MINUTES : Settings.Token.TOKEN_EXPIRE_IN_MINUTES)),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);


            return new TransferObjects.JsonWebToken
            {
                Access_Token = encodedJwt,
                Expire_In_Seconds = (int)TimeSpan.FromMinutes(Settings.Token.TOKEN_EXPIRE_IN_MINUTES).TotalSeconds,
                RefreshToken = null,
                ClientGuidId = System.Guid.NewGuid().ToString()
            };
        }


        public TransferObjects.JsonWebToken RefreshToken(string encodedJwt)
        {
            if (encodedJwt != null)
            {
                var token = new JwtSecurityTokenHandler().ReadJwtToken(encodedJwt);
                if (token != null && token.Claims != null)
                {
                    var now = DateTime.UtcNow;

                    var signingKey = GetSymmetricSecurityKey();

                    if (signingKey == null)
                        throw new ArgumentException("Unable to create SymetricSecurityKey!");

                    var jwt = new JwtSecurityToken(
                    issuer: _configuration["Token:Iss"],
                    audience: _configuration["Token:Aud"],
                    claims: token.Claims,
                    notBefore: now,
                    expires: now.Add(TimeSpan.FromMinutes(Settings.Token.TOKEN_EXPIRE_IN_MINUTES)),
                    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));
                    encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                    return new TransferObjects.JsonWebToken
                    {
                        Access_Token = encodedJwt,
                        Expire_In_Seconds = (int)TimeSpan.FromMinutes(Settings.Token.TOKEN_EXPIRE_IN_MINUTES).TotalSeconds,
                        RefreshToken = null,
                    };
                }
            }
            return null;
        }


        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            try
            {
                var now = DateTime.UtcNow;
                var symmetricKeyAsBase64 = _configuration["Token:Secret"];
                var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
                return new SymmetricSecurityKey(keyByteArray);
            }
            catch (Exception)
            {

            }
            return null;
        }

        public string TokenIssuer()
        {
            return _configuration["Token:Iss"];
        }

        public string TokenAudience()
        {
            return _configuration["Token:Aud"];
        }

        public string TokenSecretKey()
        {
            return _configuration["Token:Secret"];
        }


        JsonWebToken ITokenService.RefreshToken(string encodedJwt)
        {
            throw new NotImplementedException();
        }
    }
}
