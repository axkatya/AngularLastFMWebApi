using Newtonsoft.Json;

namespace AngularLastFMWebApi.Models
{
	/// <summary>
	/// The track entity.
	/// </summary>
	public class Track
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
		/// Gets or sets the listeners.
		/// </summary>
		/// <value>
		/// The listeners.
		/// </value>
		[JsonProperty("listeners")]
	    public int Listeners { get; set; }

		/// <summary>
		/// Gets or sets the URL.
		/// </summary>
		/// <value>
		/// The URL.
		/// </value>
		[JsonProperty("url")]
	    public string Url { get; set; }
	}
}
