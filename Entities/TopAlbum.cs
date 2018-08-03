using Newtonsoft.Json;

namespace AngularLastFMWebApi.Models
{
	public class TopAlbum
    {
	    [JsonProperty("name")]
	    public string Name { get; set; }

	    [JsonProperty("url")]
	    public string Url { get; set; }

	    [JsonProperty("image")]
	    public Image[] Image { get; set; }

	    [JsonProperty("playCount")]
	    public int PlayCount { get; set; }
	}
}
