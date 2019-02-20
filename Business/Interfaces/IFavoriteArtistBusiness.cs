using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
	/// <summary>
	/// The interface for favorite artist business.
	/// </summary>
	public interface IFavoriteArtistBusiness
	{
		/// <summary>
		/// Gets the favorite artists.
		/// </summary>
		/// <param name="userId">The user identifier.</param>
		/// <returns></returns>
		Task<IEnumerable<Artist>> GetFavoriteArtists(int userId);

		/// <summary>
		/// Gets the name of the favorite artists by.
		/// </summary>
		/// <param name="albumName">Name of the album.</param>
		/// <param name="userId">The user identifier.</param>
		/// <returns></returns>
		Task<IEnumerable<Artist>> GetFavoriteArtistsByName(string albumName, int userId);

		/// <summary>
		/// Saves the favorite artist.
		/// </summary>
		/// <param name="artist">The artist.</param>
		/// <param name="userId">The user identifier.</param>
		/// <returns></returns>
		int SaveFavoriteArtist(Artist artist, int userId);

		/// <summary>
		/// Deletes the favorite artist.
		/// </summary>
		/// <param name="favoriteAlbumId">The favorite album identifier.</param>
		void DeleteFavoriteArtist(int favoriteAlbumId);
	}
}
