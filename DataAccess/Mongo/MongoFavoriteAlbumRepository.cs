using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;
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
		
		public IEnumerable<Entities.Album> GetFavoriteAlbums(int userId)
        {
		    IEnumerable<Entities.Album> favoriteAlbums;

            var cl = _db.GetCollection<FavoriteAlbum>("FavoriteAlbum").AsQueryable<FavoriteAlbum>();

		    IEnumerable<FavoriteAlbum> favoriteAlbumsResult = IAsyncCursorSourceExtensions.ToList(cl);

		    favoriteAlbums = favoriteAlbumsResult.Select(r => new Entities.Album
		    {
                FavoriteAlbumId = Convert.ToInt32(r.Id),
                Name = r.Name,
                Artist = r.Artist,
                Url = r.Url
		    });

            return favoriteAlbums;
		}

	    public IEnumerable<Entities.Album> GetFavoriteAlbumsByName(string albumName, int userId)
	    {
	        throw new System.NotImplementedException();
	    }

	    public int GetFavoriteAlbumsByAlbumNameAndArtistName(string albumName, string artistName, int userId)
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

	    public int GetFavoriteAlbumsByGuid(string albumGuid, int userId)
	    {
	        throw new NotImplementedException();
	    }
	}
}
