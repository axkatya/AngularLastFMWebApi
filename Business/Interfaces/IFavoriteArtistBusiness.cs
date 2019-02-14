using Entities;
using System.Collections.Generic;

namespace Business.Interfaces
{
    public interface IFavoriteArtistBusiness
    {
        IEnumerable<Artist> GetFavoriteArtists(int userId);

        IEnumerable<Artist> GetFavoriteArtistsByName(string albumName, int userId);


        int SaveFavoriteArtist(Artist artist, int userId);

        void DeleteFavoriteArtist(int favoriteAlbumId);
    }
}
