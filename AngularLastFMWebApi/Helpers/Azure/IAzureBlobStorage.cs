using System.Threading.Tasks;

namespace AngularLastFMWebApi.Azure
{
	public interface IAzureBlobStorage
	{
		Task UploadAsync(string blobName, string filePath);
	}
}
