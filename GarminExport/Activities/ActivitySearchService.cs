using GarminExport.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using GarminExport.Activities.Model;
using Activity = GarminExport.Activities.Model.Activity;
using System.Diagnostics;
using RestSharp;

namespace GarminExport.Activities
{
    //https://connect.garmin.com/proxy/activity-search-service-1.2/data_ns0.html
    public class ActivitySearchService
    {
        private readonly Session Session;
        private RestClient ConnectClient { get; set; }

        public ActivitySearchService(Session session)
        {
            this.Session = session;
            ConnectClient = new RestClient("https://connect.garmin.com/")
            {
                CookieContainer = Session.Cookies,
                FollowRedirects = true
            };
        }

        public ActivitySearchResultsContainer FindActivities()
        {
            return FindActivities(new ActivitySearchFilters());
        }

        public ActivitySearchResultsContainer FindActivities(ActivitySearchFilters filters)
        {
            var loginRequest = new RestRequest("/proxy/activity-search-service-1.2/json/activities?" + filters.ToQueryString(), Method.GET);
            var response = ConnectClient.Execute(loginRequest);
            return ActivitySearchResultsContainer.ParseJson(response.Content);
        }


        public List<Activity> FindAllActivities()
        {
            return FindAllActivities(new ActivitySearchFilters());
        }

        public List<Activity> FindAllActivities(ActivitySearchFilters filters)
        {
            filters.Page = 0;

            var activities = new List<Activity>();
            ActivitySearchResultsContainer results;
            do
            {
                filters.Page++;
                Console.WriteLine("Searching page {0}", filters.Page);
                results = FindActivities(filters);
                activities.AddRange(results.Results.Activities.Select(a => a.Activity));
                Console.WriteLine("Found page {0} or {1}", results.Results.CurrentPage, results.Results.TotalPages);
            } while (results.Results.CurrentPage < results.Results.TotalPages);

            return activities;
        }
    }
}
