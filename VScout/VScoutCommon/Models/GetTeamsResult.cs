using System.Runtime.Serialization;

namespace VScoutCommon.Models
{
    [DataContract]
    public class GetTeamsResult
    {
        [DataMember(Name = "teams")]
        public Team[] Teams { get; set; }

        [DataMember(Name = "teamCountTotal")]
        public int teamCountTotal { get; set; }

        [DataMember(Name = "teamCountPage")]
        public int teamCountPage { get; set; }

        [DataMember(Name = "pageCurrent")]
        public int pageCurrent { get; set; }

        [DataMember(Name = "pageTotal")]
        public int pageTotal { get; set; }
    }

}
