using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAgent;
using System;
using System.Threading.Tasks;

namespace AngularLastFMWebApi.Controllers
{
	/// <summary>
	/// The album controller.
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
	[Route("api/[controller]")]
	public class AlbumController : Controller
	{
		private readonly ILastFmServiceAgent _lastFmServiceAgent;
		private readonly IFavoriteAlbumBusiness _favoriteAlbumBusiness;

		/// <summary>
		/// Initializes a new instance of the <see cref="AlbumController" /> class.
		/// </summary>
		/// <param name="serviceAgent">The service agent.</param>
		/// <param name="favoriteAlbumBusiness">The favorite album business.</param>
		public AlbumController(ILastFmServiceAgent serviceAgent, IFavoriteAlbumBusiness favoriteAlbumBusiness)
		{
			_lastFmServiceAgent = serviceAgent;
			_favoriteAlbumBusiness = favoriteAlbumBusiness;
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
			var albums = await _lastFmServiceAgent.GetAlbums(albumName);
			if (albums != null)
			{
				var stringUserId = Helpers.UserService.GetCurrentUserId(this.User);
				if (string.IsNullOrWhiteSpace(stringUserId))
				{
					return Unauthorized();
				}

				if (Int32.TryParse(stringUserId, out var userId))
				{
					foreach (var album in albums)
					{
						album.FavoriteAlbumId = await
							_favoriteAlbumBusiness.GetFavoriteAlbumsByAlbumNameAndArtistName(album.Name, album.Artist,
								userId);
					}
				}

				return Ok(albums);
			}

			return NoContent();
		}
	}
}
