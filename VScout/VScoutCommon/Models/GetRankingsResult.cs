using System.Runtime.Serialization;

namespace VScoutCommon.Models
{
    [DataContract]
    public class GetRankingsResult
    {
        [DataMember(Name = "Rankings")]
        public Ranking[] Rankings { get; set; }
    }
}
