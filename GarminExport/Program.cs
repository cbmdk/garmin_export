
using GarminExport.Activities;
using GarminExport.Activities.Model;
using GarminExport.Auth;
using RestSharp;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using Activity = GarminExport.Activities.Model.Activity;

namespace GarminExport
{
    class Program
    {
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

            DownloadService exportService = new DownloadService(authService.Session);
            foreach (var activity in activities)
            {
                exportService.DownloadActivity(activity, "./export");
            }
        }
    }
}
