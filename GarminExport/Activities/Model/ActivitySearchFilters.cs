using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace GarminExport.Activities.Model
{
    public class ActivitySearchFilters
    {
        public string Keyword { get; set; }
        public int? ActivityStartId { get; set; }
        public int? Page { get; set; }
        public int? ActivitiesPerPage { get; set; }
        public string SortOrder { get; set; }
        public string SortField { get; set; }
        public string Explore { get; set; }
        public bool? IgnoreNonGps { get; set; }
        public bool? IgnoreUntitled { get; set; }
        public string AggregateBy { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public string ToQueryString()
        {
            NameValueCollection parameters = new NameValueCollection();
            AddIfNotNull(parameters, "keyword", Keyword);
            AddIfNotNull(parameters, "start", ActivityStartId);
            AddIfNotNull(parameters, "currentPage", Page);
            AddIfNotNull(parameters, "limit", ActivitiesPerPage);
            AddIfNotNull(parameters, "sortField", SortField);
            AddIfNotNull(parameters, "sortOrder", SortOrder);
            AddIfNotNull(parameters, "ignoreNonGps", IgnoreNonGps);
            AddIfNotNull(parameters, "ignoreUntitled", IgnoreUntitled);
            AddIfNotNull(parameters, "aggregateBy", AggregateBy);
            AddIfNotNull(parameters, "explore", Explore);

            var specialParameters = new List<string>();
            if (FromDate.HasValue)
            {
                specialParameters.Add(String.Format("beginTimestamp>={0:s}", FromDate.Value));
            }
            if (ToDate.HasValue)
            {
                specialParameters.Add(String.Format("endTimestamp<={0:s}", ToDate.Value));
            }

            return BuildQueryString(parameters, specialParameters);
        }

        private void AddIfNotNull(NameValueCollection parameters, string name, object value)
        {
            if (value == null)
                return;
            parameters.Add(name, value.ToString());
        }

        private string BuildQueryString(NameValueCollection parameters, List<string> specialParameters)
        {
            string queryString = "";
            foreach (var key in parameters.AllKeys)
            {
                queryString += key + "=" + parameters[key] + "&";
            }

            if (specialParameters.Count == 0)
                return queryString;

            string specialQueryString = String.Join("&", specialParameters.ToArray());
            if (String.IsNullOrEmpty(queryString))
                return specialQueryString;

            return queryString + "&" + specialQueryString;
        }
    }

}
