namespace VScoutCollector.Models;

public partial class TeamRound
{
    public int TeamNumber { get; set; }

    public int MatchNumber { get; set; }

    public bool AutoMoved { get; set; }

    public bool AutoDocked { get; set; }

    public bool AutoEngaged { get; set; }

    public int AutoConeHigh { get; set; }

    public int AutoConeMiddle { get; set; }

    public int AutoConeLow { get; set; }

    public int AutoCubeHigh { get; set; }

    public int AutoCubeMiddle { get; set; }

    public int AutoCubeLow { get; set; }

    public bool Docked { get; set; }

    public bool Engaged { get; set; }

    public int ConeHigh { get; set; }

    public int ConeMiddle { get; set; }

    public int ConeLow { get; set; }

    public int CubeHigh { get; set; }

    public int CubeMiddle { get; set; }

    public int CubeLow { get; set; }

    public string Notes { get; set; }
}
