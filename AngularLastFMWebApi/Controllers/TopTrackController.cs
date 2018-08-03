using AngularLastFMWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceAgent;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AngularLastFMWebApi.Controllers
{
	[Route("api/[controller]")]
	public class TopTrackController : Controller
	{
		ILastFmServiceAgent _lastFmServiceAgent;

		public TopTrackController(ILastFmServiceAgent serviceAgent)
		{
			_lastFmServiceAgent = serviceAgent;
		}

		[HttpGet("{artistName}")]
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
