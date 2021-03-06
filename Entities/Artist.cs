﻿using Newtonsoft.Json;

namespace Entities
{
	/// <summary>
	/// The artist entity.
	/// </summary>
	public class Artist
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
		/// Gets or sets the biography.
		/// </summary>
		/// <value>
		/// The biography.
		/// </value>
		[JsonProperty("bio")]
		public Bio Bio { get; set; }

		/// <summary>
		/// Gets or sets the favorite artist id.
		/// </summary>
		/// <value>
		/// The favorite artist id.
		/// </value>
		[JsonProperty("favoriteArtistId")]
		public int FavoriteArtistId { get; set; }
	}
}
