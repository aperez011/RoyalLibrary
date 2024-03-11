using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace RL.Utility.JWT
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public sealed class AuthorizedAttribute : Attribute, IAuthorizationFilter, IAuthorizeData
    {
        private readonly IEnumerable<Claim> _claims;
        public string? Roles { get; set; }
        public string? Policy { get; set; }

        public string? AuthenticationSchemes { get; set; }

        public AuthorizedAttribute()
        {
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //var testIp = context.HttpContext.Connection.RemoteIpAddress;
            var requestIp = IPAddress.Loopback;

            var identity = context.HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            string? expirationDate = identity?.Claims.FirstOrDefault(c => c.Type.Equals("exp"))?.Value;
            DateTime date = ConvertExpirationToDateTime(expirationDate);

            string role = identity?.Claims.Single(t => t.Type == ClaimTypes.Role).Value;

            var sessionId = identity?.Claims.Single(t => t.Type == "SessionId").Value;

        }

        private DateTime ConvertExpirationToDateTime(string? timestamp)
        {
            if (string.IsNullOrWhiteSpace(timestamp)) return DateTime.Now.AddMinutes(1);

            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(long.Parse(timestamp));
        }

        private async Task<bool> ValidateId(string ip)
        {
            return await Task.FromResult(true);
        }

        private async Task PostId(string ip)
        {

        }
    }
}
