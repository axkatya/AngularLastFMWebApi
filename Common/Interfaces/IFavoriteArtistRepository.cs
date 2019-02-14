using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IFavoriteArtistRepository
    {
        IEnumerable<Entities.Artist> GetFavoriteArtists(int userId);

        IEnumerable<Entities.Artist> GetFavoriteArtistsByName(string albumName, int userId);

        int SaveFavoriteArtist(Entities.Artist artist, int userId);

        void DeleteFavoriteArtist(int favoriteArtistId);
    }
}
