using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAgent;
using System;
using System.Threading.Tasks;

namespace AngularLastFMWebApi.Controllers
{
    /// <summary>
    /// The top album controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
    public class TopAlbumController : Controller
    {
        readonly ILastFmServiceAgent _lastFmServiceAgent;
        private readonly IFavoriteAlbumBusiness _favoriteAlbumBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="TopAlbumController" /> class.
        /// </summary>
        /// <param name="serviceAgent">The service agent.</param>
        /// <param name="favoriteAlbumBusiness">The favorite album business.</param>
        public TopAlbumController(ILastFmServiceAgent serviceAgent, IFavoriteAlbumBusiness favoriteAlbumBusiness)
        {
            _lastFmServiceAgent = serviceAgent;
            _favoriteAlbumBusiness = favoriteAlbumBusiness;

        }

        /// <summary>
        /// Gets the specified artist name.
        /// </summary>
        /// <param name="artistName">Name of the artist.</param>
        /// <returns>Top Album list</returns>
        /// <response code="200">Top Album list</response>
        /// <response code="400">Error model</response> 
        [HttpGet("{artistName}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<IActionResult> Get(string artistName)
        {
            try
            {
                var topAlbums = await _lastFmServiceAgent.GetTopAlbums(artistName);
                if (topAlbums != null)
                {
                    var stringUserId = Helpers.UserService.GetCurrentUserId(this.User);
                    if (string.IsNullOrWhiteSpace(stringUserId))
                    {
                        return Unauthorized();
                    }

                    if (Int32.TryParse(stringUserId, out var userId))
                    {
                        foreach (var topAlbum in topAlbums)
                        {
                            topAlbum.FavoriteAlbumId =
                                await _favoriteAlbumBusiness.GetFavoriteAlbumsByAlbumNameAndArtistName(topAlbum.Name, artistName, userId);
                        }
                    }

                    return Ok(topAlbums);
                }

                return NoContent();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
