using AngularLastFMWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceAgent;
using System;
using System.Collections.Generic;
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
		ILastFmServiceAgent _lastFmServiceAgent;

		/// <summary>
		/// Initializes a new instance of the <see cref="AlbumController"/> class.
		/// </summary>
		/// <param name="serviceAgent">The service agent.</param>
		public AlbumController(ILastFmServiceAgent serviceAgent)
		{
			_lastFmServiceAgent = serviceAgent;
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
		public async Task<IActionResult> Get(string albumName)
		{
			var response = await _lastFmServiceAgent.GetAlbums(albumName);
			if (response != null)
			{
				return Ok(response);
			}

			return Ok(new List<Album>());
		}
	}
}
