using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using DataAccess.Interfaces;
using Entities;
using Microsoft.Extensions.Options;

namespace DataAccess.Implementation.SQL
{
    public class FavoriteAlbumRepository : IFavoriteAlbumRepository
    {
        private const string LargeImageSize = "large";
        private readonly string _connectionString;

        public FavoriteAlbumRepository(IOptions<Entities.Settings> options)
        {
            _connectionString = options.Value.SQLDBConnectionString;
        }

        /// <summary>
        /// Gets the favorite albums.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IEnumerable<Entities.Album> GetFavoriteAlbums(int userId)
        {
            IList<Entities.Album> favoriteAlbums = new List<Entities.Album>();

            string queryString =
                @"SELECT  [Id]
                ,[Name]
                ,[ArtistName]
                ,[Url]
                ,[Image]
            FROM [dbo].[FavoriteAlbum]
            WHERE [UserId] = @UserId; ";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command =
                    new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        favoriteAlbums.Add(new Entities.Album
                        {
                            FavoriteAlbumId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Artist = reader.GetString(2),
                            Url = reader.GetString(3),
                            Image = new[] { new Image { Text = reader.GetString(4), Size = LargeImageSize } }
                        });
                    }

                    reader.Close();
                }
            }

            return favoriteAlbums;
        }

        public IEnumerable<Entities.Album> GetFavoriteAlbumsByName(string albumName, int userId)
        {
            IList<Entities.Album> favoriteAlbums = new List<Entities.Album>();

            string queryString =
                @"SELECT  [Id]
                ,[Name]
                ,[ArtistName]
                ,[Url]
                ,[Image]
            FROM [dbo].[FavoriteAlbum]
            WHERE [UserId] = @UserId AND [Name] LIKE @AlbumName; ";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command =
                    new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@AlbumName", albumName);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        favoriteAlbums.Add(new Entities.Album
                        {
                            FavoriteAlbumId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Artist = reader.GetString(2),
                            Url = reader.GetString(3),
                            Image = new[] { new Image { Text = reader.GetString(4), Size = LargeImageSize } }
                        });
                    }

                    reader.Close();
                }
            }

            return favoriteAlbums;
        }

        public int GetFavoriteAlbumsByAlbumNameAndArtistName(string albumName, string artistName, int userId)
        {
            int favoriteAlbumId = 0;

            string queryString =
                @"SELECT  [Id]
            FROM [dbo].[FavoriteAlbum]
            WHERE [UserId] = @UserId AND [Name] = @AlbumName AND [ArtistName] =  @ArtistName; ";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command =
                    new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@AlbumName", albumName);
                    command.Parameters.AddWithValue("@ArtistName", artistName);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        favoriteAlbumId = reader.GetInt32(0);
                    }

                    reader.Close();
                }
            }

            return favoriteAlbumId;
        }

        public int SaveFavoriteAlbum(Entities.Album album, int userId)
        {
            int favoriteAlbumId;

            string queryString =
                @"IF NOT EXISTS 
            (SELECT  [Id]
            FROM [dbo].[FavoriteAlbum]
            WHERE [Name] = @AlbumName AND [ArtistName] = @ArtistName AND [UserId] = @UserId)
            BEGIN
                INSERT  INTO [dbo].[FavoriteAlbum] ([Name], [ArtistName], [Url], [Image], [UserId]) 
                OUTPUT INSERTED.ID
                VALUES (@AlbumName, @ArtistName, @Url, @Image, @UserId)
            END; ";

            string imageUrl;
            var image = album.Image.FirstOrDefault(i => string.Equals(i.Size, LargeImageSize));
            if (image != null)
            {
                imageUrl = image.Text;
            }
            else
            {
                image = album.Image.FirstOrDefault();
                imageUrl = image?.Text;
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@AlbumName", album.Name);
                    command.Parameters.AddWithValue("@ArtistName", album.Artist);
                    command.Parameters.AddWithValue("@Url", album.Url);
                    command.Parameters.AddWithValue("@Image", imageUrl);
                    command.Parameters.AddWithValue("@UserId", userId);
                    connection.Open();

                    favoriteAlbumId = (int)command.ExecuteScalar();

                    connection.Close();
                }
            }

            return favoriteAlbumId;
        }

        public void DeleteFavoriteAlbum(int favoriteAlbumId)
        {
            string queryString =
                @"DELETE [dbo].[FavoriteAlbum]
            WHERE [Id] = @FavoriteAlbumId; ";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@FavoriteAlbumId", favoriteAlbumId);
                    connection.Open();

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
        }
    }
}
