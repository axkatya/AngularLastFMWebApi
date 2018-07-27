using Newtonsoft.Json;

namespace AngularLastFMWebApi.Models
{
	public class ArtistSearchByNameResponse
    {
	    [JsonProperty("artist")]
		public Artist Artist { get; set; }
    }
}
