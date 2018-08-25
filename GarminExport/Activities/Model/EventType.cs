using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GarminExport.Activities.Model
{
    [DataContract]
    public class EventType
    {
        [DataMember(Name = "typeId")]
        public int TypeId { get; set; }

        [DataMember(Name = "typeKey")]
        public string TypeKey { get; set; }

        [DataMember(Name = "sortOrder")]
        public int SortOrder { get; set; }
    }
}
