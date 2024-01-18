using VScoutCommon.ResourceModels;

namespace VScoutCommon.Clients.FrcClient
{
    public interface IFrcClient
    {
        Task<Schedules> GetScheduleAsync();
        Task<string> GetScheduleJsonAsync();
        Task<List<Team>> GetTeamsAsync();
        Task<string> GetTeamsJsonAsync();
    }
}
