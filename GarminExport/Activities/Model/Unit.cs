using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GarminExport.Activities.Model
{
    [DataContract]
    public class Unit
    {
        [DataMember(Name = "unitId")]
        public int UnitId { get; set; }

        [DataMember(Name = "unitKey")]
        public string UnitKey { get; set; }

        [DataMember(Name = "factor")]
        public double Factor { get; set; }
    }
}
