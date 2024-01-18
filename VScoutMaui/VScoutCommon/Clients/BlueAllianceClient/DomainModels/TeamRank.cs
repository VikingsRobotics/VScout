namespace VScoutCommon.Clients.BlueAllianceClient.DomainModels
{
    public class TeamRank
    {
        public int TeamNumber { get; set; }

        public int Rank { get; set; }

        public int MatchesPlayed { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        public decimal OPR { get; set; }
    }
}
