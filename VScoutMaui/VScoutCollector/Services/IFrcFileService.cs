using VScoutCommon.ResourceModels;

namespace VScoutCollector.Services
{
    public interface IFrcFileService
    {
        Task<Schedules> GetSchedulesAsync();

        Task<List<Team>> GetTeamsAsync();
    }
}