using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AngularLastFMWebApi.Helpers;
using Business.Interfaces;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AngularLastFMWebApi.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountBusiness _accountBusiness;
        private readonly Settings _appSettings;

        public AccountController(IAccountBusiness accountBusiness, IOptions<Settings> appSettings)
        {
            _accountBusiness = accountBusiness;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Authenticate([FromBody]Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _accountBusiness.GetAccount(account.Username, account.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            var tokenString = GenerateJSONWebToken(user);
            return Ok(new { token = tokenString });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Register([FromBody]Account user)
        {
            try
            {
                var userId = await _accountBusiness.Create(user.Username, user.Password).ConfigureAwait(false);

                if (userId > 0)
                {
                    return Ok();
                }

                return BadRequest();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(ex.Message);
            }
        }

        private string GenerateJSONWebToken(Account user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.jWTKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            };

            var token = new JwtSecurityToken(issuer: _appSettings.jWTIssuer,
                audience: _appSettings.jWTIssuer,
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}