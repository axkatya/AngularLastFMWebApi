using DataAccess.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using DataAccess.Implementation.Mongo;
using DataAccess.Implementation.SQL;

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

        public IEnumerable<Entities.Album> GetFavoriteAlbums(int userId) => _options.Value.MongoDbEnabled ? _mongoFavoriteAlbumRepository.GetFavoriteAlbums(userId) : _sqlFavoriteAlbumRepository.GetFavoriteAlbums(userId);

        public IEnumerable<Entities.Album> GetFavoriteAlbumsByName(string albumName, int userId) =>
            _sqlFavoriteAlbumRepository.GetFavoriteAlbums(userId);

        public int SaveFavoriteAlbum(Entities.Album album, int userId) =>
            _sqlFavoriteAlbumRepository.SaveFavoriteAlbum(album, userId);

        public void DeleteFavoriteAlbum(int favoriteAlbumId) =>
            _sqlFavoriteAlbumRepository.DeleteFavoriteAlbum(favoriteAlbumId);

        public int GetFavoriteAlbumsByAlbumNameAndArtistName(string albumName, string artistName, int userId)
            => _sqlFavoriteAlbumRepository.GetFavoriteAlbumsByAlbumNameAndArtistName(albumName, artistName, userId);
    }
}
