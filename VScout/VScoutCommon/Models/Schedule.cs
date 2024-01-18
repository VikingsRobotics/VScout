using System.Runtime.Serialization;

namespace VScoutCommon.Models
{
    [DataContract]
    public class Schedule
    {
        [DataMember(Name = "Schedule")]
        public Round[] Rounds { get; set; }
    }
}
