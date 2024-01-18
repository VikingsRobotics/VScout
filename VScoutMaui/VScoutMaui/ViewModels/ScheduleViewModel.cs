using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VScoutCentral.Models;
using VScoutCentral.Services;
using VScoutCentral.Views;
using VScoutCommon.ExtensionMethods;

namespace VScoutCentral.ViewModels
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

        [RelayCommand]
        private async void OnRowSelected(RoundViewModel model)
        {
            await Shell.Current.GoToAsync($"{nameof(JustInTimeReportView)}?matchNumber={model.MatchNumber}");
        }
    }
}
