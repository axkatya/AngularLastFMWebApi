using Newtonsoft.Json;

namespace AngularLastFMWebApi.Models
{
	public class Bio
	{
		[JsonProperty("published")]
		public string Published { get; set; }

		[JsonProperty("summary")]
		public string Summary { get; set; }

		[JsonProperty("content")]
		public string Content { get; set; }
	}
}
