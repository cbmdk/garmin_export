using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GarminExport.Activities.Model
{
    [DataContract]
    public class Privacy
    {
        [DataMember(Name = "typeId")]
        public int TypeId { get; set; }

        [DataMember(Name = "typeKey")]
        public string TypeKey { get; set; }
    }
}
