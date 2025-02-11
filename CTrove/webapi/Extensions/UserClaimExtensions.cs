using System.Security.Claims;
using Microsoft.Identity.Web;
using CTrove.Api.Settings;

namespace CTrove.Api.Extensions
{
    public static class UserClaimExtensions
    {
        public static Guid GetUserObjectId(this ClaimsPrincipal principal)
        {
            Guid userId = Guid.Empty;
            string _userId = principal.GetObjectId() ?? string.Empty;

            Guid.TryParse(_userId, out userId);

            return userId;
        }
        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            string value = principal.Claims.Where(e => e.Type == ApplicationSettings.UserClaims?.UserEmail)?.FirstOrDefault()?.Value ?? string.Empty;

            return value;
        }
        public static string GetUserRole(this ClaimsPrincipal principal)
        {

            string value = principal.Claims.Where(e => e.Type == ApplicationSettings.UserClaims?.UserRole)?.FirstOrDefault()?.Value ?? string.Empty;

            return value;
        }
    }
}
