using AngularLastFMWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceAgent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AngularLastFMWebApi.Controllers
{
	[Route("api/[controller]")]
	public class AlbumController : Controller
	{
		ILastFmServiceAgent _lastFmServiceAgent;
		public AlbumController(ILastFmServiceAgent serviceAgent)
		{
			_lastFmServiceAgent = serviceAgent;
		}

		[HttpGet("{albumName}")]
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
