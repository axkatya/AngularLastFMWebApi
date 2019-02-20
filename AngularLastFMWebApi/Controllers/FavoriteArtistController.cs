using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AngularLastFMWebApi.Helpers;
using Business.Interfaces;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AngularLastFMWebApi.Controllers
{
	/// <summary>
	/// The favorite artist controller.
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
	[Produces("application/json")]
	[Route("api/[controller]")]
	public class FavoriteArtistController : Controller
	{
		private readonly IFavoriteArtistBusiness _favoriteArtistBusiness;

		/// <summary>
		/// Initializes a new instance of the <see cref="FavoriteArtistController"/> class.
		/// </summary>
		/// <param name="favoriteArtistBusiness">The favorite artist business.</param>
		public FavoriteArtistController(IFavoriteArtistBusiness favoriteArtistBusiness)
		{
			_favoriteArtistBusiness = favoriteArtistBusiness;
		}

		/// <summary>
		/// Gets favorite artists.
		/// </summary>
		/// <returns>Artist list.</returns>
		/// <response code="200">Artist list</response>
		/// <response code="400">Error model</response>
		[HttpGet]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		[Authorize]
		public async Task<IActionResult> Get()
		{
			var stringUserId = UserService.GetCurrentUserId(this.User);
			if (string.IsNullOrWhiteSpace(stringUserId))
			{
				return Unauthorized();
			}

			if (Int32.TryParse(stringUserId, out var userId))
			{
				var response = await _favoriteArtistBusiness.GetFavoriteArtists(userId).ConfigureAwait(false);
				if (response != null)
				{
					return Ok(response);
				}

				return NoContent();
			}

			return Unauthorized();
		}

		/// <summary>
		/// Gets the specified artist name.
		/// </summary>
		/// <param name="artistName">Name of the artist.</param>
		/// <returns>Album list.</returns>
		/// <response code="200">artist list</response>
		/// <response code="400">Error model</response>
		[HttpGet("{artistName}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[Authorize]
		public async Task<IActionResult> Get(string artistName)
		{
			var stringUserId = UserService.GetCurrentUserId(User);
			if (string.IsNullOrWhiteSpace(stringUserId))
			{
				return Unauthorized();
			}

			if (Int32.TryParse(stringUserId, out var userId))
			{
				var response = await _favoriteArtistBusiness.GetFavoriteArtistsByName(artistName, userId);
				if (response != null)
				{
					return Ok(response);
				}

				return NoContent();
			}

			return Unauthorized();
		}

		/// <summary>
		/// Saves the specified artist.
		/// </summary>
		/// <param name="artist">The artist.</param>
		/// <returns></returns>
		[HttpPost]
		[Authorize]
		public IActionResult Post([FromBody]Artist artist)
		{
			var stringUserId = UserService.GetCurrentUserId(User);
			if (string.IsNullOrWhiteSpace(stringUserId))
			{
				return Unauthorized();
			}

			if (Int32.TryParse(stringUserId, out var userId))
			{
				var response = _favoriteArtistBusiness.SaveFavoriteArtist(artist, userId);
				if (response > 0)
				{
					return Ok(response);
				}
			}

			return Unauthorized();
		}

		/// <summary>
		/// Deletes the specified favorite artist identifier.
		/// </summary>
		/// <param name="favoriteArtistId">The favorite artist identifier.</param>
		/// <returns></returns>
		[HttpDelete("{favoriteArtistId}")]
		[Authorize]
		public IActionResult Delete(int favoriteArtistId)
		{
			try
			{
				_favoriteArtistBusiness.DeleteFavoriteArtist(favoriteArtistId);
			}
			catch (AppException ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok(new List<Album>());
		}
	}
}
