using AngularLastFMWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceAgent;
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
		ILastFmServiceAgent _lastFmServiceAgent;

		/// <summary>
		/// Initializes a new instance of the <see cref="ArtistController"/> class.
		/// </summary>
		/// <param name="serviceAgent">The service agent.</param>
		public ArtistController(ILastFmServiceAgent serviceAgent)
		{
			_lastFmServiceAgent = serviceAgent;
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
		public async Task<IActionResult> Get(string artistName)
		{
			var response = await _lastFmServiceAgent.GetArtist(artistName);
			if (response != null)
			{
				return Ok(response);
			}

			return Ok(new Artist());
		}
	}
}
