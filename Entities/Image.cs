using Newtonsoft.Json;

namespace AngularLastFMWebApi.Models
{
	/// <summary>
	/// The image entity.
	/// </summary>
	public class Image
    {
		/// <summary>
		/// Gets or sets the image URL.
		/// </summary>
		/// <value>
		/// The image URL.
		/// </value>
		[JsonProperty("#text")]
		public string Text { get; set; }

		/// <summary>
		/// Gets or sets the image size.
		/// </summary>
		/// <value>
		/// The image size.
		/// </value>
		[JsonProperty("size")]
		public string Size { get; set; }
	}
}
