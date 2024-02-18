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
            AutoAmp = teamRound.AutoAmp;
            AutoSpeaker = teamRound.AutoSpeaker;
            
            Amp = teamRound.Amp;
            Speaker = teamRound.Speaker;

            OnStage = teamRound.OnStage;
            OnStageChain = teamRound.OnStageChain;
            NoteInOnChain = teamRound.NoteInOnChain;
            Spotlight = teamRound.Spotlight;

            Notes = teamRound.Notes;
        }

        [ObservableProperty]
        private int _teamNumber;

        [ObservableProperty]
        private int _matchNumber;

        [ObservableProperty]
        private bool _autoMoved;

        [ObservableProperty]
        private int _autoAmp;

        [ObservableProperty]
        private int _autoSpeaker;

        [ObservableProperty]
        private int _amp;

        [ObservableProperty]
        private int _speaker;

        [ObservableProperty]
        private bool _onStage;

        [ObservableProperty]
        private bool _onStageChain;

        [ObservableProperty]
        private bool _noteInOnChain;

        [ObservableProperty]
        private bool _spotlight;

        [ObservableProperty]
        private string _notes;

        [RelayCommand]
        private void AutoIncrementAmp()
        {
            AutoAmp++;
        }

        [RelayCommand]
        private void AutoDecrementAmp()
        {
            AutoAmp--;
        }

        [RelayCommand]
        private void AutoIncrementSpeaker()
        {
            AutoSpeaker++;
        }

        [RelayCommand]
        private void AutoDecrementSpeaker()
        {
            AutoSpeaker--;
        }

        [RelayCommand]
        private void IncrementAmp()
        {
            Amp++;
        }

        [RelayCommand]
        private void DecrementAmp()
        {
            Amp--;
        }

        [RelayCommand]
        private void IncrementSpeaker()
        {
            Speaker++;
        }

        [RelayCommand]
        private void DecrementSpeaker()
        {
            Speaker--;
        }

        [RelayCommand]
        private async void Save()
        {
            await _scheduleDatabaseService.SaveTeamRoundAsync(new Models.TeamRound
            {
                MatchNumber = MatchNumber,
                TeamNumber = TeamNumber,
                AutoMoved = AutoMoved,
                AutoAmp = AutoAmp,
                AutoSpeaker = AutoSpeaker,
                Amp = Amp,
                Speaker = Speaker,
                OnStage = OnStage,
                OnStageChain = OnStageChain,
                NoteInOnChain = NoteInOnChain,
                Spotlight = Spotlight,
                Notes = Notes
            });

            await Shell.Current.GoToAsync("..");
        }
    }
}
