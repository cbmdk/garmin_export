using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GarminExport.Activities.Model
{
    [DataContract]
    public class Query
    {
        [DataMember(Name = "filters")]
        public Filters Filters { get; set; }

        [DataMember(Name = "sortOrder")]
        public string SortOrder { get; set; }

        [DataMember(Name = "sortField")]
        public string SortField { get; set; }

        [DataMember(Name = "activityStart")]
        public string ActivityStart { get; set; }

        [DataMember(Name = "activitiesPerPage")]
        public string ActivitiesPerPage { get; set; }

        [DataMember(Name = "explore")]
        public string Explore { get; set; }

        [DataMember(Name = "ignoreUntitled")]
        public string IgnoreUntitled { get; set; }

        [DataMember(Name = "ignoreNonGPS")]
        public string IgnoreNonGPS { get; set; }
    }
}
