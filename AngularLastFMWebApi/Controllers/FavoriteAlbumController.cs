using AngularLastFMWebApi.Helpers;
using Business.Interfaces;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AngularLastFMWebApi.Controllers
{
	/// <summary>
	/// The favorite album controller.
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
	[Produces("application/json")]
	[Route("api/[controller]")]
	public class FavoriteAlbumController : Controller
	{
		private readonly IFavoriteAlbumBusiness _favoriteAlbumBusiness;

		/// <summary>
		/// Initializes a new instance of the <see cref="FavoriteAlbumController"/> class.
		/// </summary>
		/// <param name="favoriteAlbumBusiness">The favorite album business.</param>
		public FavoriteAlbumController(IFavoriteAlbumBusiness favoriteAlbumBusiness)
		{
			_favoriteAlbumBusiness = favoriteAlbumBusiness;
		}

		/// <summary>
		/// Gets favorite albums.
		/// </summary>
		/// <returns>Album list.</returns>
		/// <response code="200">Album list</response>
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
				var response = await _favoriteAlbumBusiness.GetFavoriteAlbums(userId);
				if (response != null)
				{
					return Ok(response);
				}

				return NoContent();
			}

			return Unauthorized();
		}

		/// <summary>
		/// Gets the specified album name.
		/// </summary>
		/// <param name="albumName">Name of the album.</param>
		/// <returns>Album list.</returns>
		/// <response code="200">Album list</response>
		/// <response code="400">Error model</response>
		[HttpGet("{albumName}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[Authorize]
		public async Task<IActionResult> Get(string albumName)
		{
			var stringUserId = UserService.GetCurrentUserId(this.User);
			if (string.IsNullOrWhiteSpace(stringUserId))
			{
				return Unauthorized();
			}

			if (Int32.TryParse(stringUserId, out var userId))
			{
				var response = await _favoriteAlbumBusiness.GetFavoriteAlbumsByName(albumName, userId);
				if (response != null)
				{
					return Ok(response);
				}

				return NoContent();
			}

			return Unauthorized();
		}

		/// <summary>
		/// Posts the specified album.
		/// </summary>
		/// <param name="album">The album.</param>
		/// <returns></returns>
		[HttpPost]
		[Authorize]
		public IActionResult Post([FromBody]Album album)
		{
			var stringUserId = UserService.GetCurrentUserId(this.User);
			if (string.IsNullOrWhiteSpace(stringUserId))
			{
				return Unauthorized();
			}

			if (Int32.TryParse(stringUserId, out var userId))
			{
				var response = _favoriteAlbumBusiness.SaveFavoriteAlbum(album, userId);
				if (response > 0)
				{
					return Ok(response);
				}
			}

			return Unauthorized();
		}

		/// <summary>
		/// Deletes the specified favorite album identifier.
		/// </summary>
		/// <param name="favoriteAlbumId">The favorite album identifier.</param>
		/// <returns></returns>
		[HttpDelete("{favoriteAlbumId}")]
		[Authorize]
		public IActionResult Delete(int favoriteAlbumId)
		{
			try
			{
				_favoriteAlbumBusiness.DeleteFavoriteAlbum(favoriteAlbumId);
			}
			catch (AppException ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok(new List<Album>());
		}
	}
}
