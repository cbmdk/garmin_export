using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GarminExport.Activities.Model
{
    [DataContract]
    public class ActivitySearchResults
    {
        [DataMember(Name = "activities")]
        public List<ActivityContainer> Activities { get; set; }

        [DataMember(Name = "totalFound")]
        public int TotalFound { get; set; }

        [DataMember(Name = "currentPage")]
        public int CurrentPage { get; set; }

        [DataMember(Name = "totalPages")]
        public int TotalPages { get; set; }

        public void Append(ActivitySearchResults results)
        {
            Activities.AddRange(results.Activities);
        }
    }
}
