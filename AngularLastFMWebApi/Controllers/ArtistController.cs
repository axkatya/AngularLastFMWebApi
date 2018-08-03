using AngularLastFMWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceAgent;
using System.Threading.Tasks;

namespace AngularLastFMWebApi.Controllers
{
	[Route("api/[controller]")]
	public class ArtistController : Controller
	{
		ILastFmServiceAgent _lastFmServiceAgent;

		public ArtistController(ILastFmServiceAgent serviceAgent)
		{
			_lastFmServiceAgent = serviceAgent;
		}

		[HttpGet("{artistName}")]
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
