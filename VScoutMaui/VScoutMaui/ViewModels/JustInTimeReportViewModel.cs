using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using VScoutCentral.Models;
using VScoutCentral.Services;

namespace VScoutCentral.ViewModels
{
    [QueryProperty(nameof(MatchNumber), "matchNumber")]
    public partial class JustInTimeReportViewModel : ObservableObject
    {
        private readonly IScheduleDatabaseService _scheduleDatabaseService;

        [ObservableProperty]
        private int _scheduleWindowRound;

        [ObservableProperty]
        private int _matchNumber;

        [ObservableProperty]
        private ObservableCollection<JustInTimeRectangeViewModel> _jitBlueTeamTiles = new ObservableCollection<JustInTimeRectangeViewModel>();


        [ObservableProperty]
        private ObservableCollection<JustInTimeRectangeViewModel> _jitRedTeamTiles = new ObservableCollection<JustInTimeRectangeViewModel>();

        public JustInTimeReportViewModel(IScheduleDatabaseService scheduleDatabaseService)
        {
            _scheduleDatabaseService = scheduleDatabaseService;
        }

        [RelayCommand]
        private async Task Appearing()
        {
            try
            {
                Task determineScheduleWindowTask = DetermineRoundWhenAllPreviousDataIsCompleteAsync();
                await PopulateTilesAsync();
                await determineScheduleWindowTask;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "OK");
            }
        }

        [RelayCommand]
        private void Disappearing()
        {
            JitRedTeamTiles.Clear();
            JitBlueTeamTiles.Clear();
        }

        private static JustInTimeRectangeViewModel GetJitRectangeViewModel(List<TeamRound> teamRounds, string station)
        {
            int roundsPlayed = teamRounds.Count(r => r.HasData);

            decimal divisor = Math.Max(roundsPlayed, 1);

            decimal averageAmp = teamRounds.Sum(r => r.AutoAmp + r.Amp) / divisor;
            decimal averageSpeaker = teamRounds.Sum(r => r.AutoSpeaker + r.Speaker) / divisor;
            string comments = string.Join(Environment.NewLine, teamRounds.Select(r => r.Notes).Reverse());
            int teamNumber = teamRounds[0].TeamNumber;
            int rank = teamRounds[0].Rank;

            return new JustInTimeRectangeViewModel
            {
                TeamNumber = teamNumber,
                RoundsPlayed = roundsPlayed,
                Rank = rank,
                Station = station,
                AverageAmp = averageAmp,
                AverageSpeaker = averageSpeaker,
                Comments = comments
            };
        }

        private async Task PopulateTilesAsync()
        {
            List<(int TeamNumber, string Station)> teamRoundInfo = await _scheduleDatabaseService.GetTeamsInRoundAsync(MatchNumber);

            foreach ((int teamNumber, string station) in teamRoundInfo)
            {
                List<TeamRound> teamRounds = await _scheduleDatabaseService.GetTeamRoundsAsync(teamNumber);

                if (station.StartsWith("Red"))
                {
                    JitRedTeamTiles.Add(GetJitRectangeViewModel(teamRounds, station));
                }
                else
                {
                    JitBlueTeamTiles.Add(GetJitRectangeViewModel(teamRounds, station));
                }
            }
        }

        public async Task DetermineRoundWhenAllPreviousDataIsCompleteAsync()
        {
            List<Round> rounds = await _scheduleDatabaseService.GetRoundsAsync();

            Round roundToSearchBefore = rounds.First(r => r.MatchNumber == MatchNumber);

            List<int> teamsToSearchFor = new List<int>
            {
                roundToSearchBefore.TeamAssignments[0].TeamNumber,
                roundToSearchBefore.TeamAssignments[1].TeamNumber,
                roundToSearchBefore.TeamAssignments[2].TeamNumber,
                roundToSearchBefore.TeamAssignments[3].TeamNumber,
                roundToSearchBefore.TeamAssignments[4].TeamNumber,
                roundToSearchBefore.TeamAssignments[5].TeamNumber,
            };

            foreach (Round round in rounds.Where(r => r.MatchNumber < MatchNumber).OrderByDescending(r => r.MatchNumber))
            {
                if (teamsToSearchFor.Contains(round.TeamAssignments[0].TeamNumber) || teamsToSearchFor.Contains(round.TeamAssignments[1].TeamNumber) || teamsToSearchFor.Contains(round.TeamAssignments[2].TeamNumber) ||
                    teamsToSearchFor.Contains(round.TeamAssignments[3].TeamNumber) || teamsToSearchFor.Contains(round.TeamAssignments[4].TeamNumber) || teamsToSearchFor.Contains(round.TeamAssignments[5].TeamNumber))
                {
                    ScheduleWindowRound = round.MatchNumber;
                    return;
                }
            }

            return;
        }
    }
}
