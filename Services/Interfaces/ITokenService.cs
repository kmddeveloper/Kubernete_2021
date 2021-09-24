
using EFModel;
using Kubernetes.TransferObjects;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kubernetes.Services
{
    public interface ITokenService
    {
        JsonWebToken CreateJWT(UserSession userSession);
        Task<JsonWebToken> CreateJWTAsync(string sessionId);
        JsonWebToken RefreshToken(string encodedJwt);
        SymmetricSecurityKey GetSymmetricSecurityKey();
        string TokenIssuer();
        string TokenAudience();
        string TokenSecretKey();
    }
}
