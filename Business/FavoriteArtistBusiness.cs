using DataAccess.Interfaces;
using Entities;
using System.Collections.Generic;
using Business.Interfaces;

namespace Business
{
    public class FavoriteArtistBusiness : IFavoriteArtistBusiness
    {
        private readonly IFavoriteArtistRepository _favoriteArtistRepository;

        public FavoriteArtistBusiness(IFavoriteArtistRepository favoriteArtistRepository)
        {
            _favoriteArtistRepository = favoriteArtistRepository;
        }
        public IEnumerable<Artist> GetFavoriteArtists(int userId)
        {
            return _favoriteArtistRepository.GetFavoriteArtists(userId);
        }

        public IEnumerable<Artist> GetFavoriteArtistsByName(string albumName, int userId)
        {
            return _favoriteArtistRepository.GetFavoriteArtistsByName(albumName, userId);
        }

        public int SaveFavoriteArtist(Artist artist, int userId)
        {
            return _favoriteArtistRepository.SaveFavoriteArtist(artist, userId);
        }

        public void DeleteFavoriteArtist(int favoriteAlbumId)
        {
            _favoriteArtistRepository.DeleteFavoriteArtist(favoriteAlbumId);
        }
    }
}
