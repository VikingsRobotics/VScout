using System.Runtime.Serialization;

namespace VScoutCommon.Models
{
    [DataContract]
    public class Team
    {
        [DataMember(Name = "teamNumber")]
        public int TeamNumber { get; set; }

        [DataMember(Name ="nameFull")]
        public string NameFull { get; set; }

        [DataMember(Name = "nameShort")]
        public string NameShort { get; set; }

        [DataMember(Name = "schoolName")]
        public string SchoolName { get; set; }

        [DataMember(Name = "city")]
        public string City { get; set; }

        [DataMember(Name = "stateProv")]
        public string StateProv { get; set; }

        [DataMember(Name = "country")]
        public string Country { get; set; }

        [DataMember(Name = "website")]
        public string Website { get; set; }

        [DataMember(Name = "rookieYear")]
        public int RookieYear { get; set; }

        [DataMember(Name = "robotName")]
        public string RobotName { get; set; }

        [DataMember(Name = "districtCode")]
        public object DistrictCode { get; set; }

        [DataMember(Name = "homeCMP")]
        public string HomeCMP { get; set; }
    }
}
