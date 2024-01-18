using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VScoutCommon.Models
{
    //public class Rootobject
    //{
    //    public Ranking[] Rankings { get; set; }
    //}
    [DataContract]
    public class Ranking
    {
        [DataMember(Name = "rank")]
        public int Rank { get; set; }

        [DataMember(Name = "teamNumber")]
        public int TeamNumber { get; set; }

        [DataMember(Name = "sortOrder1")]
        public float SortOrder1 { get; set; }

        [DataMember(Name = "sortOrder2")]
        public float SortOrder2 { get; set; }

        [DataMember(Name = "sortOrder3")]
        public float SortOrder3 { get; set; }

        [DataMember(Name = "sortOrder4")]
        public float SortOrder4 { get; set; }

        [DataMember(Name = "sortOrder5")]
        public float SortOrder5 { get; set; }

        [DataMember(Name = "sortOrder6")]
        public float SortOrder6 { get; set; }

        [DataMember(Name = "wins")]
        public int Wins { get; set; }

        [DataMember(Name = "losses")]
        public int Losses { get; set; }

        [DataMember(Name = "ties")]
        public int Ties { get; set; }

        [DataMember(Name = "qualAverage")]
        public float QualAverage { get; set; }

        [DataMember(Name = "dq")]
        public int Dq { get; set; }

        [DataMember(Name = "matchesPlayed")]
        public int MatchesPlayed { get; set; }
    }
}
