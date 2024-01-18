using VScoutCentral.Models;

namespace VScoutCentral.Services
{
    public interface IScheduleDatabaseService
    {
        Task DeleteRoundsAsync();

        Task<List<Round>> GetRoundsAsync();

        Task SaveRoundsAsync(List<Round> rounds);

        Task SaveTeamRoundAsync(TeamRound teamRound);

        Task InsertTeamsAysnc(List<Team> teams);

        Task<List<(int TeamNumber, string Station)>> GetTeamsInRoundAsync(int roundNumber);

        Task<List<TeamRound>> GetTeamRoundsAsync(int teamNumber);
        Task<List<TeamRound>> GetTeamRoundsAsync();
        Task<List<Team>> GetTeams();

        Task UpdateTeamsAsync(List<Team> teams);
    }
}