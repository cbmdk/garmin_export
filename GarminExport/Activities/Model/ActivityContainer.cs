using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GarminExport.Activities.Model
{
    [DataContract]
    public class ActivityContainer
    {
        [DataMember(Name = "activity")]
        public Activity Activity { get; set; }
    }
}
