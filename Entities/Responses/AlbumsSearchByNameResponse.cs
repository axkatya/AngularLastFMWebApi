using Newtonsoft.Json;

namespace AngularLastFMWebApi.Models
{
	public class Albummatches
	{
		[JsonProperty("album")]
		public Album[] Album { get; set; }
	}

	public class Results
	{
		[JsonProperty("albummatches")]
		public Albummatches AlbumMatches { get; set; }
	}

	public class AlbumsSearchByNameResponse
    {
	    [JsonProperty("results")]
		public Results Results { get; set; }
	}
}
