using Entities;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
	public interface IFavoriteAlbumRepository
	{
        IEnumerable<Album> GetFavoriteAlbums(int userId);

	    IEnumerable<Album> GetFavoriteAlbumsByName(string albumName, int userId);

	    int GetFavoriteAlbumsByAlbumNameAndArtistName(string albumName, string artistName, int userId);

        int SaveFavoriteAlbum(Album album, int userId);

	    void DeleteFavoriteAlbum(int favoriteAlbumId);
	}
}