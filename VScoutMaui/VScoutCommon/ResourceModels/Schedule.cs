namespace VScoutCommon.ResourceModels;

public class Schedule
{
    public string description { get; set; } = string.Empty;

    public DateTime startTime { get; set; }

    public int matchNumber { get; set; }

    public string field { get; set; } = string.Empty;

    public string tournamentLevel { get; set; } = string.Empty;

    public TeamAssignment[] teams { get; set; } = new TeamAssignment[0];
}
