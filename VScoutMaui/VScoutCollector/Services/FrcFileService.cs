using System.Text.Json;
using VScoutCollector.Utilities;
using VScoutCommon.ResourceModels;

namespace VScoutCollector.Services
{
    public class FrcFileService : IFrcFileService
    {
        public async Task<Schedules> GetSchedulesAsync()
        {
            string fileContents = await FileHelper.GetFileContentsFromUserSelectionAsync();

            return JsonSerializer.Deserialize<Schedules>(fileContents);
        }

        public async Task<List<Team>> GetTeamsAsync()
        {
            string fileContents = await FileHelper.GetFileContentsFromUserSelectionAsync();

            GetTeamsResult result = JsonSerializer.Deserialize<GetTeamsResult>(fileContents);

            return result.teams?.ToList() ?? throw new InvalidOperationException("Failed to get the teams list.");
        }
    }
}
