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

        public void DownloadActivity(Activity activity, String path)
        {
            //https://connect.garmin.com/modern/proxy/download-service/files/wellness/2018-03-24?_=1521975308450
            Console.WriteLine("Download activty {0}", activity.ActivityId);
            WebRequest webRequest = WebRequest.Create("http://connect.garmin.com/proxy/download-service/files/activity/" + activity.ActivityId);
            HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
            httpWebRequest.CookieContainer = Session.Cookies;

            var response = httpWebRequest.GetResponse();

            var targetDirectory = FileUtils.CreateDirectoryIfNotExists(path);

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

                        if (activity.StartTimeLocal != null)
                        {
                            FileInfo fileInfo = new FileInfo(filePath)
                            {
                                CreationTime = activity.StartTimeLocal,
                                LastWriteTime = activity.StartTimeLocal
                            };
                        }
                    }
                }
            }
        }

    }
}
