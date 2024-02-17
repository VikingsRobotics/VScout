namespace VScoutCentral.Models;

public partial class TeamRound
{
    public int TeamNumber { get; set; }

    public int MatchNumber { get; set; }

    public int Rank { get; set; }

    public int Wins { get; set; }

    public int Losses { get; set; }

    public decimal Opr { get; set; }

    public decimal Dpr { get; set; }

    public decimal Ccwm { get; set; }

    public bool AutoMoved { get; set; }

    public int AutoAmp { get; set; }

    public int AutoSpeaker { get; set; }

    public int Amp { get; set; }

    public int Speaker { get; set; }

    public bool OnStage { get; set; }

    public bool OnStageChain { get; set; }

    public bool NoteInOnChain { get; set; }

    public bool Spotlight { get; set; }

    public string Notes { get; set; }

    public bool HasData { get; set; }
}
