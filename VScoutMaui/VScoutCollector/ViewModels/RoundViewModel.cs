using CommunityToolkit.Mvvm.ComponentModel;

namespace VScoutCollector.ViewModels;

public partial class RoundViewModel
{
    public int MatchNumber { get; set; }
    public TeamNumber Red1TeamNumber { get; set; }
    public TeamNumber Red2TeamNumber{ get; set; }
    public TeamNumber Red3TeamNumber { get; set; }
    public TeamNumber Blue1TeamNumber{ get; set; }
    public TeamNumber Blue2TeamNumber{ get; set; }
    public TeamNumber Blue3TeamNumber { get; set; }

    public partial class TeamNumber
    {

        public int Number { get; set; }

        public bool HasData { get; set; }

        public RoundViewModel RoundViewModel { get; set; }

        public override string ToString()
        {
            return Number.ToString();
        }
    }
}
