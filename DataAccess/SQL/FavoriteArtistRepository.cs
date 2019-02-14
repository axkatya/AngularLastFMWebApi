using DataAccess.Interfaces;
using Entities;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccess.Implementation.SQL
{
    public class FavoriteArtistRepository : IFavoriteArtistRepository
    {
        private const string LargeImageSize = "large";
        private readonly string _connectionString;

        public FavoriteArtistRepository(IOptions<Entities.Settings> options)
        {
            _connectionString = options.Value.SQLDBConnectionString;
        }

        public IEnumerable<Entities.Artist> GetFavoriteArtists(int userId)
        {
            IList<Entities.Artist> favoriteArtists = new List<Entities.Artist>();

            string queryString =
                @"SELECT  [Id]
                ,[Name]
                ,[Url]
                ,[Image]
            FROM [dbo].[FavoriteArtist]
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
                        favoriteArtists.Add(new Entities.Artist
                        {
                            FavoriteArtistId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Url = reader.GetString(2),
                            Image = new[] { new Image { Text = reader.GetString(3), Size = LargeImageSize } }
                        });
                    }

                    reader.Close();
                }
            }

            return favoriteArtists;
        }

        public IEnumerable<Entities.Artist> GetFavoriteArtistsByName(string artistName, int userId)
        {
            IList<Entities.Artist> favoriteArtists = new List<Entities.Artist>();

            string queryString =
                @"SELECT  [Id]
                ,[Name]
                ,[Url]
                ,[Image]
            FROM [dbo].[FavoriteArtist]
            WHERE [UserId] = @UserId AND [Name] LIKE @ArtistName; ";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command =
                    new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@ArtistName", artistName);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        favoriteArtists.Add(new Entities.Artist
                        {
                            FavoriteArtistId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Url = reader.GetString(2),
                            Image = new[] { new Image { Text = reader.GetString(3), Size = LargeImageSize } }
                        });
                    }

                    reader.Close();
                }
            }

            return favoriteArtists;
        }

        public int SaveFavoriteArtist(Entities.Artist artist, int userId)
        {
            int favoriteArtistId;

            string queryString =
                @"IF NOT EXISTS 
            (SELECT  [Id]
            FROM [dbo].[FavoriteArtist]
            WHERE [Name] = @ArtistName AND [UserId] = @UserId)
            BEGIN
                INSERT  INTO [dbo].[FavoriteArtist] ([Name], [Url], [Image], [UserId]) 
                OUTPUT INSERTED.ID
                VALUES (@ArtistName, @Url, @Image, @UserId)
            END; ";

            string imageUrl;
            var image = artist.Image.FirstOrDefault(i => string.Equals(i.Size, LargeImageSize));
            if (image != null)
            {
                imageUrl = image.Text;
            }
            else
            {
                image = artist.Image.FirstOrDefault();
                imageUrl = image?.Text;
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@ArtistName", artist.Name);
                    command.Parameters.AddWithValue("@Url", artist.Url);
                    command.Parameters.AddWithValue("@Image", imageUrl);
                    command.Parameters.AddWithValue("@UserId", userId);
                    connection.Open();

                    favoriteArtistId = (int)command.ExecuteScalar();

                    connection.Close();
                }
            }

            return favoriteArtistId;
        }

        public void DeleteFavoriteArtist(int favoriteArtistId)
        {
            string queryString =
                @"DELETE [dbo].[FavoriteArtist]
            WHERE [Id] = @FavoriteArtistId; ";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@FavoriteArtistId", favoriteArtistId);
                    connection.Open();

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
        }
    }
}
