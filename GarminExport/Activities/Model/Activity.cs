using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GarminExport.Activities.Model
{
    [DataContract]
    public class Activity
    {
        [DataMember(Name = "activityId")]
        public string ActivityId { get; set; }

        [DataMember(Name = "activityName")]
        public string ActivityName { get; set; }

        [DataMember(Name = "activityDescription")]
        public string ActivityDescription { get; set; }

        [DataMember(Name = "activityVideoUrl")]
        public string ActivityVideoUrl { get; set; }

        [DataMember(Name = "locationName")]
        public string LocationName { get; set; }

        [DataMember(Name = "isTitled")]
        public bool IsTitled { get; set; }

        [DataMember(Name = "isElevationCorrected")]
        public bool IsElevationCorrected { get; set; }

        [DataMember(Name = "isBarometricCapable")]
        public bool IsBarometricCapable { get; set; }

        [DataMember(Name = "isSwimAlgorithmCapable")]
        public bool IsSwimAlgorithmCapable { get; set; }

        [DataMember(Name = "isVideoCapable")]
        public bool IsVideoCapable { get; set; }

        [DataMember(Name = "isActivityEdited")]
        public bool IsActivityEdited { get; set; }

        [DataMember(Name = "favorite")]
        public bool Favorite { get; set; }

        [DataMember(Name = "ispr")]
        public bool Ispr { get; set; }

        [DataMember(Name = "isAutoCalcCalories")]
        public bool IsAutoCalcCalories { get; set; }

        [DataMember(Name = "isParent")]
        public bool IsParent { get; set; }

        [DataMember(Name = "parentId")]
        public int ParentId { get; set; }

        [DataMember(Name = "userId")]
        public int UserId { get; set; }

        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "displayname")]
        public string Displayname { get; set; }

        [DataMember(Name = "uploadDate")]
        public UploadDate UploadDate { get; set; }

        [DataMember(Name = "uploadApplication")]
        public UploadApplication UploadApplication { get; set; }

        [DataMember(Name = "device")]
        public Device Device { get; set; }

        [DataMember(Name = "deviceId")]
        public string DeviceId { get; set; }

        [DataMember(Name = "isDeviceReleased")]
        public bool IsDeviceReleased { get; set; }

        [DataMember(Name = "externalId")]
        public string ExternalId { get; set; }

        [DataMember(Name = "privacy")]
        public Privacy Privacy { get; set; }

        [DataMember(Name = "numTrackpoints")]
        public int NumTrackpoints { get; set; }

        [DataMember(Name = "activityType")]
        public ActivityType ActivityType { get; set; }

        [DataMember(Name = "eventType")]
        public EventType EventType { get; set; }

        [DataMember(Name = "activityTimeZone")]
        public ActivityTimeZone ActivityTimeZone { get; set; }

        [DataMember(Name = "localizedSpeedLabel")]
        public string LocalizedSpeedLabel { get; set; }

        [DataMember(Name = "localizedPaceLabel")]
        public string LocalizedPaceLabel { get; set; }

        [DataMember(Name = "totalLaps")]
        public TotalLaps TotalLaps { get; set; }

        [DataMember(Name = "garminSwimAlgorithm")]
        public bool GarminSwimAlgorithm { get; set; }

        [DataMember(Name = "updatedDate")]
        public UpdatedDate UpdatedDate { get; set; }

        [DataMember(Name = "updatedDateFormatted")]
        public string UpdatedDateFormatted { get; set; }

        [DataMember(Name = "userRoles")]
        public IList<string> UserRoles { get; set; }

        [DataMember(Name = "deviceImageUrl")]
        public string DeviceImageUrl { get; set; }
    }
}
