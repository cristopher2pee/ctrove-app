using Azure.Core;
using CTrove.Api.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace CTrove.Api.Azure
{
    
    public class AuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public  Guid GetUserObjectId()
        {
            var claimToken = new JwtSecurityTokenHandler().ReadJwtToken(getToken());
            if (claimToken == null) return Guid.Empty;

            var identity = new ClaimsPrincipal(new ClaimsIdentity(claimToken.Claims));
            var objctId = identity.Claims.Where(f => f.Type.Equals("oid",StringComparison.OrdinalIgnoreCase))?
                .FirstOrDefault()?.Value.ToString();

            return new Guid(objctId ?? "");
        }

        public string GetUserEmail()
        {
            var claimToken = new JwtSecurityTokenHandler().ReadJwtToken(getToken());
            if (claimToken == null) return string.Empty;

            var identity = new ClaimsPrincipal(new ClaimsIdentity(claimToken.Claims));
            var email = identity.Claims.Where(f => f.Type.Equals("preferred_username", StringComparison.OrdinalIgnoreCase))?
               .FirstOrDefault()?.Value.ToString();

            return email ?? "";
        }

        private string getToken()
        {
            if(_httpContextAccessor != null)
            {
               var token = _httpContextAccessor?.HttpContext?.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                return token??"";
            }

            return string.Empty;
        }
    }
}
