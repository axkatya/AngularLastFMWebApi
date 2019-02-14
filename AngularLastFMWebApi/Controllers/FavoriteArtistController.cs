﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularLastFMWebApi.Helpers;
using Business.Interfaces;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AngularLastFMWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class FavoriteArtistController : Controller
    {
        private readonly IFavoriteArtistBusiness _favoriteArtistBusiness;

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
                var response = _favoriteArtistBusiness.GetFavoriteArtists(userId);
                if (response != null)
                {
                    return Ok(response);
                }
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
            var stringUserId = UserService.GetCurrentUserId(this.User);
            if (string.IsNullOrWhiteSpace(stringUserId))
            {
                return Unauthorized();
            }

            if (Int32.TryParse(stringUserId, out var userId))
            {
                var response = _favoriteArtistBusiness.GetFavoriteArtistsByName(artistName, userId);
                if (response != null)
                {
                    return Ok(response);
                }
            }

            return Unauthorized();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]Artist artist)
        {
            var stringUserId = UserService.GetCurrentUserId(this.User);
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

        [HttpDelete("{favoriteArtistId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int favoriteArtistId)
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
