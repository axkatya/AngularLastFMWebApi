using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
	public interface IFavoriteAlbumRepository
	{
	    Task<IEnumerable<Album>> GetFavoriteAlbums(int userId);

	    Task<IEnumerable<Album>> GetFavoriteAlbumsByName(string albumName, int userId);

	    Task<int> GetFavoriteAlbumsByAlbumNameAndArtistName(string albumName, string artistName, int userId);

	    int SaveFavoriteAlbum(Album album, int userId);

	    void DeleteFavoriteAlbum(int favoriteAlbumId);
	}
}