using System.Collections.Generic;
using Entities;

namespace Business.Interfaces
{
    public interface IFavoriteAlbumBusiness
    {
        IEnumerable<Album> GetFavoriteAlbums(int userId);

        IEnumerable<Album> GetFavoriteAlbumsByName(string albumName, int userId);

        int GetFavoriteAlbumsByAlbumNameAndArtistName(string albumName, string artistName, int userId);

        int SaveFavoriteAlbum(Album album, int userId);

        void DeleteFavoriteAlbum(int albumId);
    }
}
