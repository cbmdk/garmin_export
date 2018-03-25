using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GarminExport.Activities.Model
{
    [DataContract]
    public class UpdatedDate
    {
        [DataMember(Name = "@class")]
        public string Class { get; set; }

        [DataMember(Name = "$")]
        public string Date { get; set; }
}
}
