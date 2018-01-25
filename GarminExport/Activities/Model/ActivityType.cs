using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GarminExport.Activities.Model
{
    public class ActivityType
    {
        [DataContract]
        public class ActivityTypeContainer
        {
            [DataMember(Name = "key")]
            public string Key { get; set; }
        }
    }
}
