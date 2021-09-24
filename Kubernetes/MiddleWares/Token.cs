
using GlobalConstants;
using Kubernetes.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Web.MiddleWares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class Token
    {
        private readonly RequestDelegate _next;

        readonly ISessionService _sessionService;
        
        public Token(RequestDelegate next, ISessionService sessionService)
        {
            _next = next;
            _sessionService = sessionService;            
        }

        public Task Invoke(HttpContext httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                var sessionId = httpContext.User.FindFirst(c => c.Type == JwtRegisteredClaimNames.SessionId).Value;
              
                var userSession = _sessionService.GetAsync(sessionId).Result;

                if (userSession != null && !httpContext.Items.ContainsKey(GLOBAL_USER.USER_SESSION))
                {
                    httpContext.Items.Add(GLOBAL_USER.USER_SESSION, userSession);
                }

            }

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class TokenExtensions
    {
        public static IApplicationBuilder UseToken(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Token>();
        }
    }
}
