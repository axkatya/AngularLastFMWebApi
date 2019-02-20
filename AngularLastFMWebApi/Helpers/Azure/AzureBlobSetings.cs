using System;

namespace AngularLastFMWebApi.Azure
{
	public class AzureBlobSetings
	{
		public AzureBlobSetings(string storageConnectionString,
									   string containerName)
		{
			if (string.IsNullOrEmpty(storageConnectionString))
			{
				throw new ArgumentNullException("StorageConnectionString");
			}

			if (string.IsNullOrEmpty(containerName))
			{
				throw new ArgumentNullException("ContainerName");
			}

			StorageConnectionString = storageConnectionString;
			ContainerName = containerName;
		}

		public string StorageConnectionString { get; }

		public string ContainerName { get; }
	}
}
