using Newtonsoft.Json;

namespace AngularLastFMWebApi.Models
{
	public class Artist
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("image")]
		public Image[] Image { get; set; }

		[JsonProperty("bio")]
		public Bio Bio { get; set; }
	}
}
