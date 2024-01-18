using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VScoutCentral.Models;
using VScoutCentral.Services;
using VScoutCentral.Utilities;
using VScoutCommon.Clients.BlueAllianceClient;
using VScoutCommon.Clients.BlueAllianceClient.DomainModels;
using VScoutCommon.Clients.FrcClient;
using VScoutCommon.ResourceModels;
using Team = VScoutCommon.ResourceModels.Team;

namespace VScoutCentral.ViewModels
{
    public partial class ConfigurationViewModel : ObservableObject
    {
        private readonly FrcClient _frcClient;
        private readonly BlueAllianceClient _blueAlliance;
        private readonly IScheduleDatabaseService _scheduleService;
        private readonly IDatabaseSchemaService _databaseSchemaService;

        public ConfigurationViewModel(FrcClient frcClient, BlueAllianceClient blueAlliance, IScheduleDatabaseService scheduleService, IDatabaseSchemaService databaseSchemaService)
        {
            _frcClient = frcClient;
            _blueAlliance = blueAlliance;
            _scheduleService = scheduleService;
            _databaseSchemaService = databaseSchemaService;
        }

        [RelayCommand]
        private async void RefreshBlueAlianceStats()
        {
            try
            {
                Task<CalculatedStats> getCalculatedStatsTask = _blueAlliance.GetCalculatedStatsAsync();
                Task<List<TeamRank>> getTeamRankingsTask = _blueAlliance.GetRankingsAsync();
                List<Models.Team> teams = await _scheduleService.GetTeams();

                List<TeamRank> teamRankings = await getTeamRankingsTask;
                CalculatedStats calculatedStats = await getCalculatedStatsTask;

                foreach (TeamRank teamRank in teamRankings)
                {
                    Models.Team team = teams.First(t => t.Number == teamRank.TeamNumber);
                    team.MatchesPlayed = teamRank.MatchesPlayed;
                    team.Rank = teamRank.Rank;
                    team.Wins = teamRank.Wins;
                    team.Losses = teamRank.Losses;
                    team.Ccwm = calculatedStats.Ccwms[team.Number];
                    team.Dpr = calculatedStats.Dprs[team.Number];
                    team.Opr = calculatedStats.Oprs[team.Number];
                }

                await _scheduleService.UpdateTeamsAsync(teams);

                await Application.Current.MainPage.DisplayAlert("Success", "The Blue Alliance stats have been refreshed.", "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "OK");
            }
        }

        [RelayCommand]
        private async void DownloadSchedule()
        {
            try
            {
                bool result = await Application.Current.MainPage.DisplayAlert("Are you sure?", "This will delete the rounds that are currently in the database and overwrite them with the schedule it downloads.", "Yes", "No");

                if (!result) return;

                Task<List<Team>> getListOfTeamsTask = _frcClient.GetTeamsAsync();
                Task<Schedules> getSchedulesTask = _frcClient.GetScheduleAsync();

                Schedules schedules = await getSchedulesTask;
                List<Round> rounds = ConvertSchedulesToRounds(schedules);

                List<Team> teams = await getListOfTeamsTask;

                _databaseSchemaService.DropDatabase();
                await _databaseSchemaService.CreateDatabaseAsync();

                await _scheduleService.InsertTeamsAysnc(ConvertToDomainTeams(teams));
                Task oInsertTask = _scheduleService.SaveRoundsAsync(rounds);

                await oInsertTask;

                await Application.Current.MainPage.DisplayAlert("Download Complete", "Downloaded schedule successfully", "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "OK");
            }
        }

        [RelayCommand]
        private async Task SaveSchedule()
        {
            try
            {
                string scheduleJson = await _frcClient.GetScheduleJsonAsync();
                await FileHelper.SaveFileToUserSelectedFileAsync("schedule.json", scheduleJson);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "OK");
            }
        }

        [RelayCommand]
        private async Task SaveTeams()
        {
            try
            {
                string teamsJson = await _frcClient.GetTeamsJsonAsync();

                await FileHelper.SaveFileToUserSelectedFileAsync("teams.json", teamsJson);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "OK");
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
