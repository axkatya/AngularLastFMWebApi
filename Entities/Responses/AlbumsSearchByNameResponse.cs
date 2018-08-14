using Newtonsoft.Json;

namespace AngularLastFMWebApi.Models
{
	public class Albummatches
	{
		/// <summary>
		/// Gets or sets the album.
		/// </summary>
		/// <value>
		/// The album.
		/// </value>
		[JsonProperty("album")]
		public Album[] Album { get; set; }
	}

	public class Results
	{
		/// <summary>
		/// Gets or sets the album matches.
		/// </summary>
		/// <value>
		/// The album matches.
		/// </value>
		[JsonProperty("albummatches")]
		public Albummatches AlbumMatches { get; set; }
	}

	public class AlbumsSearchByNameResponse
    {
		/// <summary>
		/// Gets or sets the results.
		/// </summary>
		/// <value>
		/// The results.
		/// </value>
		[JsonProperty("results")]
		public Results Results { get; set; }
	}
}
