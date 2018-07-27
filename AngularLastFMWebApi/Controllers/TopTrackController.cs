using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AngularLastFMWebApi.Models;
using AngularLastFMWebApi.Models.Responses;
using Newtonsoft.Json;

namespace AngularLastFMWebApi.Controllers
{
	[Route("api/[controller]")]
	public class TopTrackController
    {
	    [HttpGet("{artistName}")]
	    public async Task<IEnumerable<Track>> Get(string artistName)
	    {
		    HttpClient client = new HttpClient();
		    client.DefaultRequestHeaders.Accept.Clear();
		    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		    client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

		    try
		    {
			    var response = await client.GetStringAsync("http://ws.audioscrobbler.com/2.0/?method=artist.gettoptracks&artist=" + artistName + "&api_key=91c70ecd632c37f12855243d9526cc6f&format=json");
			    TopTracksSearchByArtistNameResponse result = JsonConvert.DeserializeObject<TopTracksSearchByArtistNameResponse>(response);
			    if (result != null)
			    {
				    return result.TopTracks.Track;
			    }
		    }
		    catch (Exception e)
		    {
			    Console.WriteLine(e);
			    throw;
		    }

		    return new List<Track>();
	    }
	}
}
