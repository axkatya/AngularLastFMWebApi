using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccess.Implementation
{
	public class FavoriteAlbum
	{
		/// <summary>
		/// Gets or sets the internal id.
		/// </summary>
		/// <value>
		/// The internal id.
		/// </value>
		[BsonId]
		public ObjectId InternalId { get; set; }

		/// <summary>
		/// Gets or sets the id.
		/// </summary>
		/// <value>
		/// The id.
		/// </value>
		[BsonElement("id")]
		public string Id { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		[BsonElement("name")]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the artist name.
		/// </summary>
		/// <value>
		/// The artist name.
		/// </value>
		[BsonElement("artistName")]
		public string Artist { get; set; }

		/// <summary>
		/// Gets or sets the image url.
		/// </summary>
		/// <value>
		/// The image url.
		/// </value>
		public string Image { get; set; }

		/// <summary>
		/// Gets or sets the URL.
		/// </summary>
		/// <value>
		/// The URL.
		/// </value>
		[BsonElement("url")]
		public string Url { get; set; }
	}
}
