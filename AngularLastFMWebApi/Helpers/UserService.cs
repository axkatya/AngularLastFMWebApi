using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Entities;
using Microsoft.IdentityModel.Tokens;

namespace AngularLastFMWebApi.Helpers
{
    /// <summary>
    /// The user helper.
    /// </summary>
    public static class UserService
    {
        /// <summary>
        /// Gets the current user identifier.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Generates the json web token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="appSettings">The application settings.</param>
        /// <returns></returns>
        public static string GenerateJsonWebToken(Account user, Settings appSettings)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JWtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            };

            var token = new JwtSecurityToken(issuer: appSettings.JWtIssuer,
                audience: appSettings.JWtIssuer,
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
