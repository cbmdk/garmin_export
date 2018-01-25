using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GarminExport.Activities.Model
{
    [DataContract]
    public class Activity
    {
        [DataMember(Name = "activityId")]
        public long ActivityId { get; set; }

        [DataMember(Name = "activityName")]
        public string ActivityName { get; set; }

        [DataMember(Name = "activityDescription")]
        public string ActivityDescription { get; set; }

        [DataMember(Name = "activityType")]
        public ActivityType ActivityType { get; set; }

        [DataMember(Name = "uploadDate")]
        public ActivityDate UploadDate { get; set; }
    }
}
