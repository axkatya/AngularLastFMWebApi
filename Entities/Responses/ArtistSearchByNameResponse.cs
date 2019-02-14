using Newtonsoft.Json;

namespace Entities
{
	public class ArtistSearchByNameResponse
    {
	    [JsonProperty("artist")]
		public Artist Artist { get; set; }
    }
}
