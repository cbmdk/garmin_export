using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GarminExport.Activities.Model
{
    [DataContract]
    public class Filters
    {
        [DataMember(Name = "activitySummaryBeginTimestampLocal")]
        public string ActivitySummaryBeginTimestampLocal { get; set; }

        [DataMember(Name = "activitySummaryBeginTimestampLocal_oper")]
        public string ActivitySummaryBeginTimestampLocalOper { get; set; }

        [DataMember(Name = "parent_id")]
        public string ParentId { get; set; }

        [DataMember(Name = "parent_id_oper")]
        public string ParentIdOper { get; set; }

        [DataMember(Name = "userId")]
        public string UserId { get; set; }

        [DataMember(Name = "userId_oper")]
        public string UserIdOper { get; set; }
    }
}
