using Newtonsoft.Json;

namespace Entities
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
