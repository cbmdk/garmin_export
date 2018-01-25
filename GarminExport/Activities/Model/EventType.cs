using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GarminExport.Activities.Model
{
    [DataContract]
    public class EventType
    {
        [DataMember(Name = "key")]
        public string Key { get; set; }
    }
}
