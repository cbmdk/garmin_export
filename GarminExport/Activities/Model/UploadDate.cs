using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GarminExport.Activities.Model
{
    [DataContract]
    public class UploadDate
    {
        [DataMember(Name = "@class")]
        public string Class { get; set; }

        [DataMember(Name = "display")]
        public string Display { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }

        [DataMember(Name = "withDay")]
        public string WithDay { get; set; }

        [DataMember(Name = "abbr")]
        public string Abbr { get; set; }

        [DataMember(Name = "millis")]
        public string Millis { get; set; }
    }
}
