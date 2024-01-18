using Joe.Common.HttpClient;
using Microsoft.Extensions.Options;
using VScoutCommon.ResourceModels;

namespace VScoutCommon.Clients.FrcClient
{
    public class FrcClient : BaseHttpClient, IFrcClient
    {
        private readonly int _year;
        private readonly string _eventCode;

        public FrcClient(HttpClient httpClient, IOptions<FrcClientOptions> options) : base(httpClient)
        {
            _year = options.Value.Year;
            _eventCode = options.Value.EventCode;
        }

        public async Task<string> GetScheduleJsonAsync()
        {
            return await GetAsync($"https://frc-api.firstinspires.org/v2.0/{_year}/schedule/{_eventCode}?tournamentLevel=qual");
        }

        public async Task<Schedules> GetScheduleAsync()
        {
            return await GetAsync<Schedules>($"https://frc-api.firstinspires.org/v2.0/{_year}/schedule/{_eventCode}?tournamentLevel=qual");
        }

        public async Task<string> GetTeamsJsonAsync()
        {
            return await GetAsync($"https://frc-api.firstinspires.org/v2.0/{_year}/teams?eventCode={_eventCode}");
        }

        public async Task<List<Team>> GetTeamsAsync()
        {
            GetTeamsResult result = await GetAsync<GetTeamsResult>($"https://frc-api.firstinspires.org/v2.0/{_year}/teams?eventCode={_eventCode}");

            return result.teams?.ToList() ?? throw new InvalidOperationException("Failed to get the teams list.");
        }
    }
}