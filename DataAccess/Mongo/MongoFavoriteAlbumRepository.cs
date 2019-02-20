using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DataAccess.Implementation.Mongo
{
	public class MongoFavoriteAlbumRepository : IFavoriteAlbumRepository
	{
		private readonly IMongoDatabase _db;

		public MongoFavoriteAlbumRepository(IOptions<Entities.Settings> options)
		{
			if (options.Value.AzureCosmosDBConnectionString != null)
			{
				var client = new MongoClient(options.Value.AzureCosmosDBConnectionString);
				_db = client.GetDatabase(options.Value.AzureCosmosDatabaseBookmarks);
			}
		}

		public async Task<IEnumerable<Album>> GetFavoriteAlbums(int userId)
		{
			IEnumerable<Entities.Album> favoriteAlbums = null;

			var cl = _db.GetCollection<FavoriteAlbum>("FavoriteAlbum").AsQueryable<FavoriteAlbum>();

			IEnumerable<FavoriteAlbum> favoriteAlbumsResult = IAsyncCursorSourceExtensions.ToList(cl);

			if (favoriteAlbumsResult != null)
			{
				favoriteAlbums = favoriteAlbumsResult.Select(r => new Entities.Album
				{
					FavoriteAlbumId = Convert.ToInt32(r.Id),
					Name = r.Name,
					Artist = r.Artist,
					Url = r.Url
				});
			}

			return favoriteAlbums;
		}

		public Task<IEnumerable<Album>> GetFavoriteAlbumsByName(string albumName, int userId) => throw new System.NotImplementedException();

		public Task<int> GetFavoriteAlbumsByAlbumNameAndArtistName(string albumName, string artistName, int userId)
		{
			throw new System.NotImplementedException();
		}

		public int SaveFavoriteAlbum(Entities.Album album, int userId)
		{
			throw new System.NotImplementedException();
		}

		public void DeleteFavoriteAlbum(int favoriteAlbumId)
		{
			throw new System.NotImplementedException();
		}
	}
}
