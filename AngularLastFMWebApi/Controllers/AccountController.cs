using AngularLastFMWebApi.Helpers;
using Business.Interfaces;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace AngularLastFMWebApi.Controllers
{
	/// <summary>
	/// The account controller.
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
	[Route("api/[controller]")]
	public class AccountController : Controller
	{
		private readonly IAccountBusiness _accountBusiness;
		private readonly Settings _appSettings;

		/// <summary>
		/// Initializes a new instance of the <see cref="AccountController"/> class.
		/// </summary>
		/// <param name="accountBusiness">The account business.</param>
		/// <param name="appSettings">The application settings.</param>
		public AccountController(IAccountBusiness accountBusiness, IOptions<Settings> appSettings)
		{
			_accountBusiness = accountBusiness;
			_appSettings = appSettings.Value;
		}

		/// <summary>
		/// Authenticates the specified account.
		/// </summary>
		/// <param name="account">The account.</param>
		/// <returns></returns>
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

			var tokenString = UserService.GenerateJsonWebToken(user, _appSettings);
			return Ok(new { token = tokenString });
		}

		/// <summary>
		/// Registers the specified user.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		[AllowAnonymous]
		[HttpPost("register")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> Register([FromBody]Account user)
		{
			try
			{
				var userId = await _accountBusiness.Create(user.Username, user.Password);

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
	}
}