namespace VScoutCommon.ResourceModels;

public class TeamAssignment
{
    public int teamNumber { get; set; }
    public string station { get; set; } = string.Empty;
    public bool surrogate { get; set; }
}
