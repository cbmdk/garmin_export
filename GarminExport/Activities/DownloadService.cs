using GarminExport.Activities.Model;
using GarminExport.Auth;
using ICSharpCode.SharpZipLib.Zip;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace GarminExport.Activities
{
    public class DownloadService
    {
        private readonly Session Session;

        public DownloadService(Session session)
        {
            this.Session = session;
        }

        public void DownloadActivity(string url, string outputPath, DateTime creationTime)
        {
            Console.WriteLine("Download url {0}", url);
            WebRequest webRequest = WebRequest.Create(url);
            HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
            httpWebRequest.CookieContainer = Session.Cookies;

            var response = httpWebRequest.GetResponse();

            var targetDirectory = FileUtils.CreateDirectoryIfNotExists(outputPath);

            using (ZipInputStream inputStream = new ZipInputStream(response.GetResponseStream()))
            {
                ZipEntry theEntry;
                while ((theEntry = inputStream.GetNextEntry()) != null)
                {
                    if (!theEntry.IsFile)
                        continue;

                    string fileName = Path.GetFileName(theEntry.Name);
                    if (fileName != String.Empty)
                    {
                        string filePath = targetDirectory.FullName + "/" + theEntry.Name;
                        using (FileStream streamWriter = File.Create(filePath))
                        {
                            int size = 2048;
                            byte[] data = new byte[2048];
                            while (true)
                            {
                                size = inputStream.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }

                        if (creationTime != null)
                        {
                            FileInfo fileInfo = new FileInfo(filePath)
                            {
                                CreationTime = creationTime,
                                LastWriteTime = creationTime
                            };
                        }
                    }
                }
            }
        }

    }
}
