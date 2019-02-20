using System.Data.SqlClient;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Microsoft.Extensions.Options;

namespace DataAccess.Implementation
{
	public class AccountRepository : IAccountRepository
	{
		private readonly string _connectionString;

		public AccountRepository(IOptions<Entities.Settings> options)
		{
			_connectionString = options.Value.SQLDBConnectionString;
		}

		public async Task<int> GetUser(string login, string password)
		{
			int userId = 0;

			const string queryString =
				@"SELECT  [Id]
			FROM [dbo].[User]
			WHERE [Login] = @Login AND [Password] = @Password; ";

			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				using (SqlCommand command = new SqlCommand(queryString, connection))
				{
					command.Parameters.AddWithValue("@Login", login);
					command.Parameters.AddWithValue("@Password", password);
					connection.Open();

					using (SqlDataReader reader = await command.ExecuteReaderAsync())
					{
						if (await reader.ReadAsync())
						{
							userId = reader.GetInt32(0);
						}

						reader.Close();
					}

					connection.Close();
				}
			}

			return userId;
		}

		public async Task<int> Create(string login, string password)
		{
			int userId;

			const string queryString =
				@"IF NOT EXISTS 
			(SELECT  [Id]
			FROM [dbo].[User]
			WHERE [Login] = @Login)
			BEGIN
				INSERT  INTO [dbo].[User] ([Login], [Password]) VALUES (@Login, @Password)
			END; ";

			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				using (SqlCommand command = new SqlCommand(queryString, connection))
				{
					command.Parameters.AddWithValue("@Login", login);
					command.Parameters.AddWithValue("@Password", password);
					connection.Open();

					userId = await command.ExecuteNonQueryAsync().ConfigureAwait(false);

					connection.Close();
				}
			}

			return userId;
		}
	}
}
