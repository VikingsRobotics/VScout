using System;
using System.Runtime.Serialization;

namespace VScoutCommon.Models
{
    [DataContract(Name = "Schedule")]
    public class Round
    {
        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "field")]
        public string Field { get; set; }

        [DataMember(Name = "tournamentLevel")]
        public string TournamentLevel { get; set; }

        [DataMember(Name = "startTime")]
        public DateTime StartTime { get; set; }

        [DataMember(Name = "matchNumber")]
        public int MatchNumber { get; set; }

        [DataMember(Name = "teams")]
        public TeamAssignment[] Teams { get; set; }
    }
}
