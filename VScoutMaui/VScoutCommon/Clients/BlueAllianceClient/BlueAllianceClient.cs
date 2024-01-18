using Joe.Common;
using Joe.Common.HttpClient;
using Microsoft.Extensions.Options;
using System.Text.Json;
using VScoutCommon.Clients.BlueAllianceClient.DomainModels;
using VScoutCommon.ResourceModels.BlueAllianceClient;

namespace VScoutCommon.Clients.BlueAllianceClient
{
    /// <summary>
    /// This app is Powered by The Blue Alliance. (thebluealliance.com)
    /// </summary>
    public class BlueAllianceClient : BaseHttpClient
    {
        private readonly int _year;
        private readonly string _eventCode;

        public BlueAllianceClient(HttpClient httpClient, IOptions<BlueAllianceClientOptions> options) : base(httpClient)
        {
            _year = options.Value.Year;
            _eventCode = options.Value.EventCode;
        }

        public async Task<string> GetOprsStringAsync()
        {
            return await GetAsync($"https://www.thebluealliance.com/api/v3/event/{_year + _eventCode.ToLower()}/oprs");
        }

        public async Task<CalculatedStats> GetCalculatedStatsAsync()
        {
            string statsJson = await GetOprsStringAsync();

            CalculatedStats stats = new CalculatedStats
            {
                Ccwms = GetStatsDictionary(statsJson, "ccwms"),
                Dprs = GetStatsDictionary(statsJson, "dprs"),
                Oprs = GetStatsDictionary(statsJson, "oprs")
            };

            return stats;
        }

        private Dictionary<int, decimal> GetStatsDictionary(string json, string stat)
        {
            Dictionary<int, decimal> values = new Dictionary<int, decimal>();

            using (JsonDocument document = JsonDocument.Parse(json))
            {
                JsonElement ccwms = document.RootElement.GetProperty(stat);
                foreach (JsonProperty property in ccwms.EnumerateObject())
                {
                    int teamNumber = Convert.ToInt32(property.Name.Substring(3));
                    decimal value = Convert.ToDecimal(property.Value.ToString().Substring(0, 10));
                    values.Add(teamNumber, value);
                }
            }

            return values;
        }

        public async Task<string> GetRankingsStringAsync()
        {
            return await GetAsync($"https://www.thebluealliance.com/api/v3/event/{_year + _eventCode.ToLower()}/rankings");
        }

        public async Task<List<TeamRank>> GetRankingsAsync()
        {
            Rankings rankings = await GetAsync<Rankings>($"https://www.thebluealliance.com/api/v3/event/{_year + _eventCode.ToLower()}/rankings") ?? throw new InvalidOperationException("Could not get rankings.");

            List<TeamRank> result = new List<TeamRank>();

            foreach (Ranking ranking in rankings.rankings)
            {
                result.Add(new TeamRank
                {
                    TeamNumber = JoeConvert.ToInt32(ranking.team_key!.Substring(3)),
                    Rank = ranking.rank,
                    MatchesPlayed = ranking.matches_played,
                    Wins = ranking.record!.wins,
                    Losses = ranking.record!.losses
                });
            }

            return result;
        }
    }
}
