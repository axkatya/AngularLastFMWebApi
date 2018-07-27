using Newtonsoft.Json;

namespace AngularLastFMWebApi.Models
{
	public class TopAlbumsSearchByArtistNameResponse
    {
	    [JsonProperty("topalbums")]
		public TopAlbums TopAlbums { get; set; }
    }

	public class TopAlbums
	{
		[JsonProperty("album")]
		public TopAlbum[] Album { get; set; }
	}
}

