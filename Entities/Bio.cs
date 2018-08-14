using Newtonsoft.Json;

namespace AngularLastFMWebApi.Models
{
	/// <summary>
	/// The biography entity.
	/// </summary>
	public class Bio
	{
		/// <summary>
		/// Gets or sets the published.
		/// </summary>
		/// <value>
		/// The published.
		/// </value>
		[JsonProperty("published")]
		public string Published { get; set; }

		/// <summary>
		/// Gets or sets the summary.
		/// </summary>
		/// <value>
		/// The summary.
		/// </value>
		[JsonProperty("summary")]
		public string Summary { get; set; }

		/// <summary>
		/// Gets or sets the content.
		/// </summary>
		/// <value>
		/// The content.
		/// </value>
		[JsonProperty("content")]
		public string Content { get; set; }
	}
}
