namespace VScoutCentral.Models
{
    public class TeamAssignment
    {
        public int TeamNumber { get; set; }

        public string Station { get; set; } = string.Empty;

        public bool HasData { get; set; }
    }
}
