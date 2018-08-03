using Newtonsoft.Json;

namespace AngularLastFMWebApi.Models
{
	public class Track
    {
	    [JsonProperty("name")]
	    public string Name { get; set; }

	    [JsonProperty("listeners")]
	    public int Listeners { get; set; }

	    [JsonProperty("url")]
	    public string Url { get; set; }
	}
}
