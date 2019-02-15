using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAgent;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AngularLastFMWebApi.Controllers
{
    /// <summary>
    /// The artist controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
	public class ArtistController : Controller
	{
	    readonly ILastFmServiceAgent _lastFmServiceAgent;
	    readonly IFavoriteArtistBusiness _favoriteArtistBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistController" /> class.
        /// </summary>
        /// <param name="serviceAgent">The service agent.</param>
        /// <param name="favoriteArtistBusiness">The favorite artist business.</param>
        public ArtistController(ILastFmServiceAgent serviceAgent, IFavoriteArtistBusiness favoriteArtistBusiness)
		{
			_lastFmServiceAgent = serviceAgent;
		    _favoriteArtistBusiness = favoriteArtistBusiness;
        }

		/// <summary>
		/// Gets the specified artist name.
		/// </summary>
		/// <param name="artistName">Name of the artist.</param>
		/// <returns>Artist list</returns>
		/// <response code="200">Artist list</response>
		/// <response code="400">Error model</response> 
		[HttpGet("{artistName}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
        [Authorize]
		public async Task<IActionResult> Get(string artistName)
		{
			var artist = await _lastFmServiceAgent.GetArtist(artistName);
			if (artist != null)
			{
			    var stringUserId = Helpers.UserService.GetCurrentUserId(this.User);
			    if (string.IsNullOrWhiteSpace(stringUserId))
			    {
			        return Unauthorized();
			    }

                if (Int32.TryParse(stringUserId, out var userId))
                {
                    var response = _favoriteArtistBusiness.GetFavoriteArtistsByName(artist.Name, userId);
                    int? favoriteArtistId = response?.FirstOrDefault()?.FavoriteArtistId;
                    if (favoriteArtistId != null)
                    {
                        artist.FavoriteArtistId = (int) favoriteArtistId;
                    }
                }
                return Ok(artist);
			}

		    return NoContent();
        }
	}
}
