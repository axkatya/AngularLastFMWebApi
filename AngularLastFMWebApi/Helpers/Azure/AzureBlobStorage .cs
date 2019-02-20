using System.IO;
using System.Threading.Tasks;
using AngularLastFMWebApi.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AngularLastFMWebApi.Helpers.Azure
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
			CloudBlockBlob blockBlob = await GetBlockBlobAsync(blobName);

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
			CloudStorageAccount.TryParse(settings.StorageConnectionString, out CloudStorageAccount storageAccount);

			CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

			CloudBlobContainer blobContainer = blobClient.GetContainerReference(settings.ContainerName);
			await blobContainer.CreateIfNotExistsAsync();
			//await blobContainer.SetPermissionsAsync(new BlobContainerPermissions() { PublicAccess = BlobContainerPublicAccessType.Blob });
			return blobContainer;
		}

		private async Task<CloudBlockBlob> GetBlockBlobAsync(string blobName)
		{
			CloudBlobContainer blobContainer = await GetContainerAsync();

			CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(blobName);

			return blockBlob;
		}

		#endregion
	}
}
