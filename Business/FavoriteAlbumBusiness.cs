using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<IEnumerable<Album>> GetFavoriteAlbums(int userId)
        {
            return await _favoriteAlbumRepository.GetFavoriteAlbums(userId);
        }

        public async Task<IEnumerable<Album>> GetFavoriteAlbumsByName(string albumName, int userId)
        {
            return await _favoriteAlbumRepository.GetFavoriteAlbumsByName(albumName, userId);
        }

        public Task<int> GetFavoriteAlbumsByAlbumNameAndArtistName(string albumName, string artistName, int userId)
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
