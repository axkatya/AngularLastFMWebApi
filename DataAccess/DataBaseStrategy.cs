using DataAccess.Implementation.Mongo;
using DataAccess.Implementation.SQL;
using DataAccess.Interfaces;
using Entities;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Implementation
{
    public class DataBaseStrategy : IFavoriteAlbumRepository
    {
        private readonly MongoFavoriteAlbumRepository _mongoFavoriteAlbumRepository;
        private readonly FavoriteAlbumRepository _sqlFavoriteAlbumRepository;
        private IOptions<Entities.Settings> _options;

        public DataBaseStrategy(IOptions<Entities.Settings> options,
            MongoFavoriteAlbumRepository mongoFavoriteAlbumRepository,
            FavoriteAlbumRepository sqlFavoriteAlbumRepository)
        {
            _mongoFavoriteAlbumRepository = mongoFavoriteAlbumRepository;
            _sqlFavoriteAlbumRepository = sqlFavoriteAlbumRepository;
            _options = options;
        }

        public async Task<IEnumerable<Album>> GetFavoriteAlbums(int userId) => _options.Value.MongoDbEnabled ? await _mongoFavoriteAlbumRepository.GetFavoriteAlbums(userId) : await _sqlFavoriteAlbumRepository.GetFavoriteAlbums(userId);

        public async Task<IEnumerable<Entities.Album>> GetFavoriteAlbumsByName(string albumName, int userId) =>
           await _sqlFavoriteAlbumRepository.GetFavoriteAlbumsByName(albumName, userId);

        public int SaveFavoriteAlbum(Entities.Album album, int userId) =>
            _sqlFavoriteAlbumRepository.SaveFavoriteAlbum(album, userId);

        public void DeleteFavoriteAlbum(int favoriteAlbumId) =>
            _sqlFavoriteAlbumRepository.DeleteFavoriteAlbum(favoriteAlbumId);

        public async Task<int> GetFavoriteAlbumsByAlbumNameAndArtistName(string albumName, string artistName, int userId)
            => await _sqlFavoriteAlbumRepository.GetFavoriteAlbumsByAlbumNameAndArtistName(albumName, artistName, userId);
    }
}
