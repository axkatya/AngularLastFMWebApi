using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using AngularLastFMWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AngularLastFMWebApi.Controllers
{
	[Route("api/[controller]")]
	public class AlbumController : Controller
	{
		[HttpGet("{albumName}")]
		public async Task<IEnumerable<Album>> Get(string albumName)
		{
			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

			var response = await client.GetStringAsync("http://ws.audioscrobbler.com/2.0/?method=album.search&album=" + albumName + "&api_key=91c70ecd632c37f12855243d9526cc6f&format=json");
			AlbumsSearchByNameResponse result = JsonConvert.DeserializeObject<AlbumsSearchByNameResponse>(response);
			if (result != null)
			{
				return result.Results.AlbumMatches.Album;
			}

			return new List<Album>();
		}
	}
}
