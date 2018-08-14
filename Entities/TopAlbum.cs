using Newtonsoft.Json;

namespace AngularLastFMWebApi.Models
{
	/// <summary>
	/// The top album entity.
	/// </summary>
	public class TopAlbum
    {
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		[JsonProperty("name")]
	    public string Name { get; set; }

		/// <summary>
		/// Gets or sets the URL.
		/// </summary>
		/// <value>
		/// The URL.
		/// </value>
		[JsonProperty("url")]
	    public string Url { get; set; }

		/// <summary>
		/// Gets or sets the image.
		/// </summary>
		/// <value>
		/// The image.
		/// </value>
		[JsonProperty("image")]
	    public Image[] Image { get; set; }

		/// <summary>
		/// Gets or sets the play count.
		/// </summary>
		/// <value>
		/// The play count.
		/// </value>
		[JsonProperty("playCount")]
	    public int PlayCount { get; set; }
	}
}
