namespace VScoutCentral.Models
{
    public class Team
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public string Name { get; set; }

        public int Rank { get; set; }

        public int MatchesPlayed { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        public decimal Ccwm { get; set; }

        public decimal Dpr { get; set; }

        public decimal Opr { get; set; }
    }
}
