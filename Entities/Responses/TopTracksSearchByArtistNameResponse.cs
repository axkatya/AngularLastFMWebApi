using Newtonsoft.Json;

namespace AngularLastFMWebApi.Models.Responses
{
	public class TopTracksSearchByArtistNameResponse
    {
	    [JsonProperty("toptracks")]
		public TopTracks TopTracks { get; set; }
	}

	public class TopTracks
	{
		[JsonProperty("track")]
		public Track[] Track { get; set; }
	}
}
