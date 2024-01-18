using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VScoutCollector.Models;
using VScoutCollector.Services;
using VScoutCommon.ResourceModels;
using Team = VScoutCommon.ResourceModels.Team;

namespace VScoutCollector.ViewModels
{
    public partial class ConfigurationViewModel : ObservableObject
    {
        private readonly IFrcFileService _frcFileService;
        private readonly IScheduleDatabaseService _scheduleService;
        private readonly IDatabaseSchemaService _databaseSchemaService;

        public ConfigurationViewModel(IFrcFileService frcFileService, IScheduleDatabaseService scheduleService, IDatabaseSchemaService databaseSchemaService)
        {
            _frcFileService = frcFileService;
            _scheduleService = scheduleService;
            _databaseSchemaService = databaseSchemaService;
        }

        [RelayCommand]
        private async void DownloadSchedule()
        {
            try
            {
                bool result = await Application.Current.MainPage.DisplayAlert("Are you sure?", "This will delete the rounds that are currently in the database and overwrite them with the schedule it downloads.", "Yes", "No");

                if (!result) return;

                await Application.Current.MainPage.DisplayAlert("Schedule", "Choose Schedule file", "OK");

                Schedules schedules = await _frcFileService.GetSchedulesAsync();
                List<Round> rounds = ConvertSchedulesToRounds(schedules);

                await Application.Current.MainPage.DisplayAlert("Teams", "Choose Teams file", "OK");

                List<Team> teams = await _frcFileService.GetTeamsAsync();

                _databaseSchemaService.DropDatabase();
                await _databaseSchemaService.CreateDatabaseAsync();

                await _scheduleService.SaveTeamsAysnc(ConvertToDomainTeams(teams));
                Task oInsertTask = _scheduleService.SaveRoundsAsync(rounds);

                await oInsertTask;

                await Application.Current.MainPage.DisplayAlert("Download Complete", "Downloaded schedule successfully", "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "OK");
                throw;
            }
        }

        private static List<Round> ConvertSchedulesToRounds(Schedules schedules)
        {
            List<Round> rounds = new List<Round>();

            foreach (Schedule schedule in schedules.Schedule)
            {
                Round round = new Round
                {
                    Field = schedule.field,
                    MatchNumber = schedule.matchNumber,
                    StartTime = schedule.startTime,
                    TournamentLevel = schedule.tournamentLevel
                };

                foreach (VScoutCommon.ResourceModels.TeamAssignment teamAssignment in schedule.teams)
                {
                    round.TeamAssignments.Add(new Models.TeamAssignment
                    {
                        Station = teamAssignment.station,
                        TeamNumber = teamAssignment.teamNumber
                    });
                }

                rounds.Add(round);
            }

            return rounds;
        }

        private static List<Models.Team> ConvertToDomainTeams(List<Team> teamsRM)
        {
            return teamsRM.Select(t => new Models.Team
            {
                Name = t.nameShort,
                Number = t.teamNumber
            }).ToList();
        }
    }
}
