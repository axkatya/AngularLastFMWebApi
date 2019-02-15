using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Entities;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;

namespace DataAccess.Implementation.SQL.Search
{
    public class SQLSearchFavoriteAlbumRepository : IFavoriteAlbumRepository
    {
        private static SearchIndexClient CreateSearchIndexClient()
        {
            string searchServiceName = "training-lasfm-dev";//configuration["SearchServiceName"];
            string queryApiKey = "C05E9A387DEED52157F6F17A3E16AE11";//configuration["SearchServiceQueryApiKey"];

            SearchIndexClient indexClient = new SearchIndexClient(searchServiceName, "azuresearchindex", new SearchCredentials(queryApiKey));
            return indexClient;
        }

        public async Task<IEnumerable<Album>> GetFavoriteAlbums(int userId)
        {
            var favoriteAlbums = new List<Entities.Album>();
            var indexClient = CreateSearchIndexClient();
            var sp = new SearchParameters();
            DocumentSearchResult<Entities.Album> response = await indexClient.Documents.SearchAsync<Entities.Album>("*", sp);
            if (response?.Results != null && response.Results.Count > 0)
            {
                favoriteAlbums = response.Results.Select(r => new Entities.Album
                {
                    FavoriteAlbumId = r.Document.FavoriteAlbumId,
                    Name = r.Document.Name,
                    Artist = r.Document.Artist,
                    Url = r.Document.Url
                }).ToList();
            }

            return favoriteAlbums;
        }

        public Task<IEnumerable<Album>> GetFavoriteAlbumsByName(string albumName, int userId)
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

        Task<int> IFavoriteAlbumRepository.GetFavoriteAlbumsByAlbumNameAndArtistName(string albumName, string artistName, int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
