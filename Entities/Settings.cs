namespace Entities
{
	public class Settings
	{
		public string AzureBlobStorageConnectionString { get; set; }
		public string AzureBlobStorageContainerName { get; set; }

		public string AzureSQLDBConnectionString { get; set; }

		public string SQLDBConnectionString { get; set; }

		public string AzureCosmosDBConnectionString { get; set; }

		public string AzureCosmosDatabaseBookmarks { get; set; }

		public bool MongoDbEnabled { get; set; }

		public string JWtKey { get; set; }

		public string JWtIssuer { get; set; }
	}
}
