using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GarminExport.Activities.Model
{
    [DataContract]
    public class ActivitySearchResultsContainer
    {
        [DataMember(Name = "results")]
        public ActivitySearchResults Results { get; set; }

        public static ActivitySearchResultsContainer ParseJson(string json)
        {
            return JsonConvert.DeserializeObject<ActivitySearchResultsContainer>(json);
        }
    }
}
