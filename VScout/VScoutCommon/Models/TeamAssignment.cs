using System.Runtime.Serialization;

namespace VScoutCommon.Models
{
    [DataContract(Name = "team")]
    public class TeamAssignment
    {
        [DataMember(Name = "teamNumber")]
        public int TeamNumber { get; set; }

        [DataMember(Name = "station")]
        public string Station { get; set; }

        [DataMember(Name = "surrogate")]
        public bool Surrogate { get; set; }
    }
}
