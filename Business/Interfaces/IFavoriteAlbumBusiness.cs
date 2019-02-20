using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Business.Interfaces
{
	/// <summary>
	/// The interface for favorite album business.
	/// </summary>
	public interface IFavoriteAlbumBusiness
	{
		/// <summary>
		/// Gets the favorite albums.
		/// </summary>
		/// <param name="userId">The user identifier.</param>
		/// <returns></returns>
		Task<IEnumerable<Album>> GetFavoriteAlbums(int userId);

		/// <summary>
		/// Gets favorite albums by name.
		/// </summary>
		/// <param name="albumName">Name of the album.</param>
		/// <param name="userId">The user identifier.</param>
		/// <returns></returns>
		Task<IEnumerable<Album>> GetFavoriteAlbumsByName(string albumName, int userId);

		/// <summary>
		/// Gets favorite albums by album name and artist.
		/// </summary>
		/// <param name="albumName">Name of the album.</param>
		/// <param name="artistName">Name of the artist.</param>
		/// <param name="userId">The user identifier.</param>
		/// <returns></returns>
		Task<int> GetFavoriteAlbumsByAlbumNameAndArtistName(string albumName, string artistName, int userId);

		/// <summary>
		/// Saves the favorite album.
		/// </summary>
		/// <param name="album">The album.</param>
		/// <param name="userId">The user identifier.</param>
		/// <returns></returns>
		int SaveFavoriteAlbum(Album album, int userId);

		/// <summary>
		/// Deletes the favorite album.
		/// </summary>
		/// <param name="albumId">The album identifier.</param>
		void DeleteFavoriteAlbum(int albumId);
	}
}
