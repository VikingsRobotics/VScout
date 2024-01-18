using VScoutCollector.Models;

namespace VScoutCollector.Services
{
    public interface IScheduleDatabaseService
    {
        Task DeleteRoundsAsync();
        Task<List<Round>> GetRoundsAsync();
        Task SaveRoundsAsync(List<Round> rounds);
        Task SaveTeamsAysnc(List<Team> teams);
        Task SaveTeamRoundAsync(TeamRound teamRound);
        Task<TeamRound> GetTeamRoundAsync(int matchNumber, int teamNumber);
    }
}