using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GarminExport.Activities.Model
{
    [DataContract]
    public class SummarizedDiveInfo
    {
        [DataMember(Name = "weight")]
        public object Weight;

        [DataMember(Name = "weightUnit")]
        public object WeightUnit;

        [DataMember(Name = "visibility")]
        public object Visibility;

        [DataMember(Name = "visibilityUnit")]
        public object VisibilityUnit;

        [DataMember(Name = "surfaceCondition")]
        public object SurfaceCondition;

        [DataMember(Name = "current")]
        public object Current;

        [DataMember(Name = "waterType")]
        public object WaterType;

        [DataMember(Name = "waterDensity")]
        public object WaterDensity;

        [DataMember(Name = "summarizedDiveGases")]
        public object[] SummarizedDiveGases;

        [DataMember(Name = "totalSurfaceTime")]
        public int TotalSurfaceTime;
    }
}
