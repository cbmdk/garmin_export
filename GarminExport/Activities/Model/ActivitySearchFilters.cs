using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace GarminExport.Activities.Model
{
    public class ActivitySearchFilters
    {
        public string ActivityType { get; set; } //running
        public string SortBy { get; set; } //startLocal
        public string SortOrder { get; set; } //asc
        public int? Limit { get; set; } //20
        public int? Start { get; set; } //0
        public int? ActivityStartId { get; set; }
        public string Search { get; set; } //keyword 
        public DateTime? StartDate { get; set; } //yyyy-MM-dd
        public DateTime? EndDate { get; set; } //yyyy-MM-dd

        public string ToQueryString()
        {
            //?startDate=2018-03-24&sortBy=startLocal&sortOrder=asc&limit=20&start=0
            NameValueCollection parameters = new NameValueCollection();
            AddIfNotNull(parameters, "activityType", ActivityType);
            AddIfNotNull(parameters, "sortBy", SortBy);
            AddIfNotNull(parameters, "sortOrder", SortOrder);
            AddIfNotNull(parameters, "limit", Limit);
            AddIfNotNull(parameters, "start", Start);
            AddIfNotNull(parameters, "_", ActivityStartId);
            AddIfNotNull(parameters, "search", Search);

            var specialParameters = new List<string>();
            if (StartDate.HasValue)
            {
                specialParameters.Add("startDate=" + StartDate.Value.ToString("yyyy-MM-dd"));
            }
            if (EndDate.HasValue)
            {
                specialParameters.Add("endDate=" + EndDate.Value.ToString("yyyy-MM-dd"));
            }

            return BuildQueryString(specialParameters, parameters);
        }

        private void AddIfNotNull(NameValueCollection parameters, string name, object value)
        {
            if (value == null)
                return;
            parameters.Add(name, value.ToString());
        }

        private string BuildQueryString(List<string> specialParameters, NameValueCollection parameters)
        {
            string queryString = String.Join("&", parameters.AllKeys.Select(key => key + "=" + parameters[key]));

            if (specialParameters.Count == 0)
                return queryString;

            string specialQueryString = String.Join("&", specialParameters.ToArray());
            if (String.IsNullOrEmpty(queryString))
                return specialQueryString;

            return specialQueryString + "&" + queryString;
        }
    }
}
