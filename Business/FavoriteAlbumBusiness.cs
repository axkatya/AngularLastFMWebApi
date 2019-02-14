using Entities;
using System.Collections.Generic;
using Business.Interfaces;
using DataAccess.Interfaces;

namespace Business
{
    public class FavoriteAlbumBusiness: IFavoriteAlbumBusiness
    {
        private readonly IFavoriteAlbumRepository _favoriteAlbumRepository;

        public FavoriteAlbumBusiness(IFavoriteAlbumRepository favoriteAlbumRepository)
        {
            _favoriteAlbumRepository = favoriteAlbumRepository;
        }
        public IEnumerable<Album> GetFavoriteAlbums(int userId)
        {
            return _favoriteAlbumRepository.GetFavoriteAlbums(userId);
        }

        public IEnumerable<Album> GetFavoriteAlbumsByName(string albumName, int userId)
        {
            return _favoriteAlbumRepository.GetFavoriteAlbumsByName(albumName, userId);
        }

        public int GetFavoriteAlbumsByAlbumNameAndArtistName(string albumName, string artistName, int userId)
        {
            return _favoriteAlbumRepository.GetFavoriteAlbumsByAlbumNameAndArtistName(albumName, artistName, userId);
        }

        public int SaveFavoriteAlbum(Album album, int userId)
        {
            return _favoriteAlbumRepository.SaveFavoriteAlbum(album, userId);
        }

        public void DeleteFavoriteAlbum(int favoriteAlbumId)
        {
            _favoriteAlbumRepository.DeleteFavoriteAlbum(favoriteAlbumId);
        }
    }
}
