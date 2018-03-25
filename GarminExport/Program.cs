
using GarminExport.Activities;
using GarminExport.Activities.Model;
using GarminExport.Auth;
using RestSharp;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using Activity = GarminExport.Activities.Model.Activity;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace GarminExport
{
    class Program
    {
        private const string EXPORT_DIR = "./export";

        static void Main(string[] args)
        {

            var result = CommandLine.Parser.Default.ParseArguments<Options>(args);
            if (result.Errors.Any())
            {
                CommandLine.Text.HelpText.AutoBuild<Options>(result);
                ReadLineIfDebug();
                return;
            }

            new Program().Run(result.Value);
        }
        private static void ReadLineIfDebug()
        {
#if (DEBUG)
            if (System.Diagnostics.Debugger.IsAttached)
                Console.ReadLine();
#endif
        }

        public void Run(Options options)
        {
            AuthService authService = new AuthService(options.UserName, options.Password);
            if (!authService.SignIn())
            {
                Console.WriteLine("Login failed");
                return;
            }

            DateTime fromDate = DateTime.MinValue;
            if(options.FromDate != null)
            {
                fromDate = DateTime.Parse(options.FromDate);
            }

            ActivitySearchService activitySearchService = new ActivitySearchService(authService.Session);
            var searchOptions = new ActivitySearchFilters
            {
                FromDate = fromDate,
                SortOrder = "asc"
            };

            Console.WriteLine("Search for activities from " + fromDate);

            List<Activity> activities = activitySearchService.FindAllActivities(searchOptions);
            if (activities == null)
            {
                Console.WriteLine("No activities found.");
                return;
            }

            if (options.SaveActivityResult)
            {
                Console.WriteLine("Save activities json");
                foreach (Activity activity in activities)
                {
                    var targetDirectory = FileUtils.CreateDirectoryIfNotExists(EXPORT_DIR);
                    var json = JsonConvert.SerializeObject(activity);
                    string filePath = targetDirectory.FullName + "/" + activity.ActivityId + ".json";
                    using (FileStream streamWriter = File.Create(filePath))
                    {
                        var data = Encoding.ASCII.GetBytes(json);
                        streamWriter.Write(data, 0, data.Length);
                    }
                    DateTime uploadTime = DateTime.Parse(activity.UploadDate.Display);
                    FileInfo fileInfo = new FileInfo(filePath);
                    fileInfo.CreationTime = uploadTime;
                    fileInfo.LastWriteTime = uploadTime;
                }
            }

            DownloadService exportService = new DownloadService(authService.Session);
            foreach (var activity in activities)
            {
                try
                {
                    exportService.DownloadActivity(activity, EXPORT_DIR);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Download activity failed:" + ex.Message);
                }
            }
        }
    }
}
