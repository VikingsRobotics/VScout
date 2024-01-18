using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Text;
using VScoutCollector.Models;
using VScoutCollector.Services;
using VScoutCollector.Views;
using VScoutCommon.ExtensionMethods;

namespace VScoutCollector.ViewModels
{
    public partial class ScheduleViewModel : ObservableObject
    {
        private readonly IScheduleDatabaseService _scheduleService;

        [ObservableProperty]
        private ObservableCollection<RoundViewModel> _rounds;

        public ScheduleViewModel(IScheduleDatabaseService scheduleService)
        {
            _scheduleService = scheduleService;
            Rounds = new ObservableCollection<RoundViewModel>();
        }

        [RelayCommand]
        private async void Appearing()
        {
            try
            {
                Rounds.Clear();

                List<Round> rounds = await _scheduleService.GetRoundsAsync();
                List<RoundViewModel> roundViewModels = new List<RoundViewModel>(rounds.Count);
                foreach (Round round in rounds)
                {
                    RoundViewModel roundViewModel = new RoundViewModel
                    {
                        MatchNumber = round.MatchNumber,
                        Red1TeamNumber = GetTeamNumber(round, "Red1"),
                        Red2TeamNumber = GetTeamNumber(round, "Red2"),
                        Red3TeamNumber = GetTeamNumber(round, "Red3"),
                        Blue1TeamNumber = GetTeamNumber(round, "Blue1"),
                        Blue2TeamNumber = GetTeamNumber(round, "Blue2"),
                        Blue3TeamNumber = GetTeamNumber(round, "Blue3"),
                    };

                    roundViewModel.Red1TeamNumber.RoundViewModel = roundViewModel;
                    roundViewModel.Red2TeamNumber.RoundViewModel = roundViewModel;
                    roundViewModel.Red3TeamNumber.RoundViewModel = roundViewModel;
                    roundViewModel.Blue1TeamNumber.RoundViewModel = roundViewModel;
                    roundViewModel.Blue2TeamNumber.RoundViewModel = roundViewModel;
                    roundViewModel.Blue3TeamNumber.RoundViewModel = roundViewModel;

                    roundViewModels.Add(roundViewModel);
                }

                Rounds.AddRange(roundViewModels);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "OK");
            }
        }

        [RelayCommand]
        private async void CellTapped(RoundViewModel.TeamNumber teamNumber)
        {
            await Shell.Current.GoToAsync($"{nameof(ScoutFormView)}?teamNumber={teamNumber.Number}&matchNumber={teamNumber.RoundViewModel.MatchNumber}");
        }

        public async Task ShowQRCodeAsync(List<int> matchNumbers)
        {
            List<TeamRound> teamRounds = new List<TeamRound>(matchNumbers.Count);

            foreach (int matchNumber in matchNumbers)
            {
                RoundViewModel round = Rounds.First(r => r.MatchNumber == matchNumber);

                if (round.Red1TeamNumber.HasData)
                {
                    teamRounds.Add(await _scheduleService.GetTeamRoundAsync(matchNumber, round.Red1TeamNumber.Number));
                }

                if (round.Red2TeamNumber.HasData)
                {
                    teamRounds.Add(await _scheduleService.GetTeamRoundAsync(matchNumber, round.Red2TeamNumber.Number));
                }

                if (round.Red3TeamNumber.HasData)
                {
                    teamRounds.Add(await _scheduleService.GetTeamRoundAsync(matchNumber, round.Red3TeamNumber.Number));
                }

                if (round.Blue1TeamNumber.HasData)
                {
                    teamRounds.Add(await _scheduleService.GetTeamRoundAsync(matchNumber, round.Blue1TeamNumber.Number));
                }

                if (round.Blue2TeamNumber.HasData)
                {
                    teamRounds.Add(await _scheduleService.GetTeamRoundAsync(matchNumber, round.Blue2TeamNumber.Number));
                }

                if (round.Blue3TeamNumber.HasData)
                {
                    teamRounds.Add(await _scheduleService.GetTeamRoundAsync(matchNumber, round.Blue3TeamNumber.Number));
                }
            }

            string serializedRounds = SerializeTeamRounds(teamRounds);
            await Shell.Current.GoToAsync($"{nameof(QRCodeView)}?qrCodeValue={serializedRounds}");
        }

        private static RoundViewModel.TeamNumber GetTeamNumber(Round round, string station)
        {
            TeamAssignment teamAssignment = round.TeamAssignments.FirstOrDefault(t => t.Station == station);

            if (teamAssignment == null) return new RoundViewModel.TeamNumber();

            return new RoundViewModel.TeamNumber
            {
                Number = teamAssignment.TeamNumber,
                HasData = teamAssignment.HasData
            };
        }

        private static string SerializeTeamRounds(List<TeamRound> teamRounds)
        {
            StringBuilder sb = new StringBuilder();
            foreach (TeamRound teamRound in teamRounds)
            {
                sb.Append(SerializeTeamRound(teamRound) + "$");
            }

            return sb.ToString();
        }

        private static string SerializeTeamRound(TeamRound teamRound)
        {
            return $"{teamRound.MatchNumber}*{teamRound.TeamNumber}*{teamRound.AutoMoved}*{teamRound.AutoDocked}*{teamRound.AutoEngaged}*" +
                   $"{teamRound.AutoConeHigh}*{teamRound.AutoConeMiddle}*{teamRound.AutoConeLow}*" +
                   $"{teamRound.AutoCubeHigh}*{teamRound.AutoCubeMiddle}*{teamRound.AutoCubeLow}*" +
                   $"{teamRound.Docked}*{teamRound.Engaged}*" +
                   $"{teamRound.ConeHigh}*{teamRound.ConeMiddle}*{teamRound.ConeLow}*" +
                   $"{teamRound.CubeHigh}*{teamRound.CubeMiddle}*{teamRound.CubeLow}*" +
                   $"{teamRound.Notes}";
        }
    }
}
