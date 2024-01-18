namespace VScoutCentral.Models
{
    public class Round
    {
        public int Id { get; set; }

        public DateTime StartTime { get; set; }

        public int MatchNumber { get; set; }

        public string Field { get; set; } = string.Empty;

        public string TournamentLevel { get; set; } = string.Empty;

        public List<TeamAssignment> TeamAssignments { get; set; } = new List<TeamAssignment>();
    }
}
