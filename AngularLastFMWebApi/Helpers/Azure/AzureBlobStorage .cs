using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AngularLastFMWebApi.Azure
{
	public class AzureBlobStorage : IAzureBlobStorage
	{
		#region Public

		public AzureBlobStorage(AzureBlobSetings settings)
		{
			this.settings = settings;
		}

		public async Task UploadAsync(string blobName, string filePath)
		{
			//Blob
			CloudBlockBlob blockBlob = await GetBlockBlobAsync(blobName);

			//Upload
			using (var fileStream = System.IO.File.Open(filePath, FileMode.Open))
			{
				fileStream.Position = 0;
				await blockBlob.UploadFromStreamAsync(fileStream);
			}
		}

		#endregion

		#region Private

		private readonly AzureBlobSetings settings;

		private async Task<CloudBlobContainer> GetContainerAsync()
		{
			//Account
			CloudStorageAccount storageAccount;
			CloudStorageAccount.TryParse(settings.StorageConnectionString, out storageAccount);

			//Client
			CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

			//Container
			CloudBlobContainer blobContainer = blobClient.GetContainerReference(settings.ContainerName);
			await blobContainer.CreateIfNotExistsAsync();
			//await blobContainer.SetPermissionsAsync(new BlobContainerPermissions() { PublicAccess = BlobContainerPublicAccessType.Blob });

			return blobContainer;
		}

		private async Task<CloudBlockBlob> GetBlockBlobAsync(string blobName)
		{
			//Container
			CloudBlobContainer blobContainer = await GetContainerAsync();

			//Blob
			CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(blobName);

			return blockBlob;
		}

		#endregion
	}
}
