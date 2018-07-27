using Newtonsoft.Json;

namespace AngularLastFMWebApi.Models
{
	public class Album
    {
	    [JsonProperty("name")]
		public string Name { get; set; }

	    [JsonProperty("artist")]
		public string Artist { get; set; }

	    [JsonProperty("url")]
		public string Url { get; set; }

	    [JsonProperty("image")]
		public Image[] Image { get; set; }
    }
}
