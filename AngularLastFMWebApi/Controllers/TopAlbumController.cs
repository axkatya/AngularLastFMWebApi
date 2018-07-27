using AngularLastFMWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AngularLastFMWebApi.Controllers
{
	[Route("api/[controller]")]
	public class TopAlbumController : Controller
	{
		[HttpGet("{artistName}")]
		public async Task<IEnumerable<TopAlbum>> Get(string artistName)
		{
			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

			try
			{
				var response = await client.GetStringAsync("http://ws.audioscrobbler.com/2.0/?method=artist.gettopalbums&artist=" + artistName + "&api_key=91c70ecd632c37f12855243d9526cc6f&format=json");
				TopAlbumsSearchByArtistNameResponse result = JsonConvert.DeserializeObject<TopAlbumsSearchByArtistNameResponse>(response);
				if (result != null)
				{
					return result.TopAlbums.Album;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}

			return new List<TopAlbum>();
		}
	}
}
