using AngularLastFMWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceAgent;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AngularLastFMWebApi.Controllers
{
	/// <summary>
	/// The top track controller.
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
	[Route("api/[controller]")]
	public class TopTrackController : Controller
	{
		ILastFmServiceAgent _lastFmServiceAgent;

		/// <summary>
		/// Initializes a new instance of the <see cref="TopTrackController"/> class.
		/// </summary>
		/// <param name="serviceAgent">The service agent.</param>
		public TopTrackController(ILastFmServiceAgent serviceAgent)
		{
			_lastFmServiceAgent = serviceAgent;
		}

		/// <summary>
		/// Gets the specified artist name.
		/// </summary>
		/// <param name="artistName">Name of the artist.</param>
		/// <returns>Top Track list</returns>
		/// <response code="200">Top Track list</response>
		/// <response code="400">Error model</response> 
		[HttpGet("{artistName}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> Get(string artistName)
		{
			try
			{
				var response = await _lastFmServiceAgent.GetTopTracks(artistName);
				if (response != null)
				{
					return Ok(response);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}

			return Ok(new List<Track>());
		}
	}
}
