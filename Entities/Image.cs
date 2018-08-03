using Newtonsoft.Json;

namespace AngularLastFMWebApi.Models
{
	public class Image
    {
	    [JsonProperty("#text")]
		public string Text { get; set; }

	    [JsonProperty("size")]
		public string Size { get; set; }
	}
}
