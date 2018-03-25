﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GarminExport.Activities.Model
{
    [DataContract]
    public class Parent
    {
        [DataMember(Name = "display")]
        public string Display { get; set; }

        [DataMember(Name = "key")]
        public string Key { get; set; }

        [DataMember(Name = "fieldNameDisplay")]
        public string FieldNameDisplay { get; set; }
    }
}