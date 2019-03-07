
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
        private const string ACTIVITY_DIR = "/activities";
        private const string WELLNESS_DIR = "/wellness";

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
            if (options.FromDate != null)
            {
                fromDate = DateTime.Parse(options.FromDate);
            }


            DownloadService downloadService = new DownloadService(authService.Session);

            if (options.SyncType.ToLower() == "all" || options.SyncType.ToLower() == "activites")
            {
                SnycActivities(options, fromDate, authService.Session, downloadService);
            }
            if (options.SyncType.ToLower() == "all" || options.SyncType.ToLower() == "wellness")
            {
                if (fromDate == null || fromDate <= DateTime.MinValue)
                    fromDate = DateTime.Today;

                SnycWellness(options, fromDate, downloadService);
            }
        }

        private void SnycActivities(Options options, DateTime fromDate, Session session, DownloadService downloadService)
        {
            ActivitySearchService activitySearchService = new ActivitySearchService(session);
            var searchOptions = new ActivitySearchFilters
            {
                StartDate = fromDate
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
                    var targetDirectory = FileUtils.CreateDirectoryIfNotExists(options.OutputPath + ACTIVITY_DIR);
                    var json = JsonConvert.SerializeObject(activity);
                    string filePath = targetDirectory.FullName + "/" + activity.ActivityId + ".json";
                    using (FileStream streamWriter = File.Create(filePath))
                    {
                        var data = Encoding.ASCII.GetBytes(json);
                        streamWriter.Write(data, 0, data.Length);
                    }
                    FileInfo fileInfo = new FileInfo(filePath);
                    fileInfo.CreationTime = activity.StartTimeLocal;
                    fileInfo.LastWriteTime = activity.StartTimeLocal;
                }
            }

            foreach (var activity in activities)
            {
                try
                {
                    downloadService.DownloadActivity("http://connect.garmin.com/proxy/download-service/files/activity/" + activity.ActivityId, options.OutputPath + ACTIVITY_DIR, activity.StartTimeLocal);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Download activity failed:" + ex.Message);
                }
            }
        }

        private void SnycWellness(Options options, DateTime fromDate, DownloadService downloadService)
        {
            while (fromDate <= DateTime.Now)
            {
                string dateString = fromDate.ToString("yyyy-MM-dd");
                string url = "https://connect.garmin.com/modern/proxy/download-service/files/wellness/" + dateString;

                try
                {
                    downloadService.DownloadActivity(url, options.OutputPath + WELLNESS_DIR, fromDate);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Download welness failed:" + ex.Message);
                }

                fromDate = fromDate.AddDays(1);
            }
        }
    }
}
