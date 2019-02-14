using System.Linq;
using System.Security.Claims;

namespace AngularLastFMWebApi.Helpers
{
    public static class UserService
    {
        public static string GetCurrentUserId(ClaimsPrincipal user)
        {
            string userId = string.Empty;
            var identity = (ClaimsIdentity)user?.Identity;
            if (identity != null)
            {
                userId = identity.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            }

            return userId;
        }
    }
}
