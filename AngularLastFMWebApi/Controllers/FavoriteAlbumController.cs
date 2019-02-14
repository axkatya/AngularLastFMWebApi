using System;
using Business.Interfaces;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AngularLastFMWebApi.Helpers;

namespace AngularLastFMWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class FavoriteAlbumController : Controller
    {
        private readonly IFavoriteAlbumBusiness _favoriteAlbumBusiness;

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
                var response = _favoriteAlbumBusiness.GetFavoriteAlbums(userId);
                if (response != null)
                {
                    return Ok(response);
                }
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
                var response = _favoriteAlbumBusiness.GetFavoriteAlbumsByName(albumName, userId);
                if (response != null)
                {
                    return Ok(response);
                }
            }

            return Unauthorized();
        }

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

        [HttpDelete("{favoriteAlbumId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int favoriteAlbumId)
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
