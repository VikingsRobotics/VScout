using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VScoutCollector.Services;

namespace VScoutCollector.ViewModels
{
    [QueryProperty(nameof(TeamNumber), "teamNumber")]
    [QueryProperty(nameof(MatchNumber), "matchNumber")]
    public partial class ScoutFormViewModel : ObservableObject
    {
        private readonly IScheduleDatabaseService _scheduleDatabaseService;

        public ScoutFormViewModel(IScheduleDatabaseService scheduleDatabaseService)
        {
            _scheduleDatabaseService = scheduleDatabaseService;
        }

        [RelayCommand]
        private async void Appearing()
        {
            Models.TeamRound teamRound = await _scheduleDatabaseService.GetTeamRoundAsync(MatchNumber, TeamNumber);
            AutoMoved = teamRound.AutoMoved;
            AutoDocked = teamRound.AutoDocked;
            AutoEngaged = teamRound.AutoEngaged;
            AutoConeHigh = teamRound.AutoConeHigh;
            AutoConeMiddle = teamRound.AutoConeMiddle;
            AutoConeLow = teamRound.AutoConeLow;
            AutoCubeHigh = teamRound.AutoCubeHigh;
            AutoCubeMiddle = teamRound.AutoCubeMiddle;
            AutoCubeLow = teamRound.AutoCubeLow;

            ConeHigh = teamRound.ConeHigh;
            ConeMiddle = teamRound.ConeMiddle;
            ConeLow = teamRound.ConeLow;
            CubeHigh = teamRound.CubeHigh;
            CubeMiddle = teamRound.CubeMiddle;
            CubeLow = teamRound.CubeLow;
            Docked = teamRound.Docked;
            Engaged = teamRound.Engaged;

            Notes = teamRound.Notes;
        }

        [ObservableProperty]
        private int _teamNumber;

        [ObservableProperty]
        private int _matchNumber;

        [ObservableProperty]
        private bool _autoMoved;

        [ObservableProperty]
        private bool _autoDocked;

        [ObservableProperty]
        private bool _autoEngaged;

        [ObservableProperty]
        private int _autoConeHigh;

        [ObservableProperty]
        private int _autoConeMiddle;

        [ObservableProperty]
        private int _autoConeLow;

        [ObservableProperty]
        private int _autoCubeHigh;

        [ObservableProperty]
        private int _autoCubeMiddle;

        [ObservableProperty]
        private int _autoCubeLow;

        [ObservableProperty]
        private bool _docked;

        [ObservableProperty]
        private bool _engaged;

        [ObservableProperty]
        private int _coneHigh;

        [ObservableProperty]
        private int _coneMiddle;

        [ObservableProperty]
        private int _coneLow;

        [ObservableProperty]
        private int _cubeHigh;

        [ObservableProperty]
        private int _cubeMiddle;

        [ObservableProperty]
        private int _cubeLow;

        [ObservableProperty]
        private string _notes;

        [RelayCommand]
        private void AutoIncrementConeHigh()
        {
            AutoConeHigh++;
        }

        [RelayCommand]
        private void AutoDecrementConeHigh()
        {
            AutoConeHigh--;
        }

        [RelayCommand]
        private void AutoIncrementConeMiddle()
        {
            AutoConeMiddle++;
        }

        [RelayCommand]
        private void AutoDecrementConeMiddle()
        {
            AutoConeMiddle--;
        }

        [RelayCommand]
        private void AutoIncrementConeLow()
        {
            AutoConeLow++;
        }

        [RelayCommand]
        private void AutoDecrementConeLow()
        {
            AutoConeLow--;
        }

        [RelayCommand]
        private void AutoIncrementCubeHigh()
        {
            AutoCubeHigh++;
        }

        [RelayCommand]
        private void AutoDecrementCubeHigh()
        {
            AutoCubeHigh--;
        }

        [RelayCommand]
        private void AutoIncrementCubeMiddle()
        {
            AutoCubeMiddle++;
        }

        [RelayCommand]
        private void AutoDecrementCubeMiddle()
        {
            AutoCubeMiddle--;
        }

        [RelayCommand]
        private void AutoIncrementCubeLow()
        {
            AutoCubeLow++;
        }

        [RelayCommand]
        private void AutoDecrementCubeLow()
        {
            AutoCubeLow--;
        }

        [RelayCommand]
        private void IncrementConeHigh()
        {
            ConeHigh++;
        }

        [RelayCommand]
        private void DecrementConeHigh()
        {
            ConeHigh--;
        }

        [RelayCommand]
        private void IncrementConeMiddle()
        {
            ConeMiddle++;
        }

        [RelayCommand]
        private void DecrementConeMiddle()
        {
            ConeMiddle--;
        }

        [RelayCommand]
        private void IncrementConeLow()
        {
            ConeLow++;
        }

        [RelayCommand]
        private void DecrementConeLow()
        {
            ConeLow--;
        }

        [RelayCommand]
        private void IncrementCubeHigh()
        {
            CubeHigh++;
        }

        [RelayCommand]
        private void DecrementCubeHigh()
        {
            CubeHigh--;
        }

        [RelayCommand]
        private void IncrementCubeMiddle()
        {
            CubeMiddle++;
        }

        [RelayCommand]
        private void DecrementCubeMiddle()
        {
            CubeMiddle--;
        }

        [RelayCommand]
        private void IncrementCubeLow()
        {
            CubeLow++;
        }

        [RelayCommand]
        private void DecrementCubeLow()
        {
            CubeLow--;
        }

        [RelayCommand]
        private async void Save()
        {
            await _scheduleDatabaseService.SaveTeamRoundAsync(new Models.TeamRound
            {
                MatchNumber = MatchNumber,
                TeamNumber = TeamNumber,
                AutoMoved = AutoMoved,
                AutoConeHigh = AutoConeHigh,
                AutoConeMiddle = AutoConeMiddle,
                AutoConeLow = AutoConeLow,
                AutoCubeHigh = AutoCubeHigh,
                AutoCubeMiddle = AutoCubeMiddle,
                AutoCubeLow = AutoCubeLow,
                AutoDocked = AutoDocked,
                AutoEngaged = AutoEngaged,
                ConeHigh = ConeHigh,
                ConeMiddle = ConeMiddle,
                ConeLow = ConeLow,
                CubeHigh = CubeHigh,
                CubeMiddle = CubeMiddle,
                CubeLow = CubeLow,
                Docked = Docked,
                Engaged = Engaged,
                Notes = Notes
            });

            await Shell.Current.GoToAsync("..");
        }
    }
}
