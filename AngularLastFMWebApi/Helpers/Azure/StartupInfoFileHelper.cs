using System;
using System.IO;

namespace AngularLastFMWebApi.Azure
{
	public class StartupInfoFileHelper
    {
		/// <summary>
		/// Creates the startup file.
		/// </summary>
		/// <returns></returns>
		public static (string, string) CreateStartupFile()
		{
			string localPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			string localFileName = "HelloWorld_" + string.Format("{0:yyyy-MM-dd_hh-mm-ss-fff}", DateTime.Now) + ".txt";
			string sourceFile = Path.Combine(localPath, localFileName);
			File.WriteAllText(sourceFile, $"DateTime: {DateTime.Now}, Server name: {System.Security.Principal.WindowsIdentity.GetCurrent().Name}");
			return (localFileName, sourceFile);
		}
	}
}
