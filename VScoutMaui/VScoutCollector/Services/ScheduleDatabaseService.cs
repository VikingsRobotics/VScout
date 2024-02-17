using Joe.Common;
using Joe.Common.DataAccess;
using System.Data;
using VScoutCollector.Models;

namespace VScoutCollector.Services
{
    internal class ScheduleDatabaseService : IScheduleDatabaseService
    {
        public async Task DeleteRoundsAsync()
        {
            using JoeSqliteConnection connection = GetOpenConnection();
            await connection.ExecuteNonQueryAsync("DELETE FROM Round; " +
                                                  "DELETE FROM TeamAssignment; ");
        }

        public async Task SaveTeamsAysnc(List<Team> teams)
        {
            using JoeSqliteConnection connection = GetOpenConnection();

            JoeSqlParameterCollection parameters = new JoeSqlParameterCollection();
            foreach (Team team in teams)
            {
                parameters.Clear();
                parameters.Add("$TeamNumber", team.Number);
                parameters.Add("$TeamName", team.Name);

                await connection.ExecuteNonQueryAsync("INSERT INTO Team(TeamNumber, TeamName) " +
                                                      "VALUES ($TeamNumber, $TeamName) ", parameters);
            }
        }

        public async Task<List<Round>> GetRoundsAsync()
        {
            using JoeSqliteConnection connection = GetOpenConnection();

            using DataTable roundTable = await connection.GetDataTableAsync("SELECT A.* " +
                                                                            "FROM Round A; ");

            List<Round> rounds = new List<Round>(roundTable.Rows.Count);

            foreach (DataRow row in roundTable.Rows)
            {
                Round round = new Round
                {
                    Id = JoeConvert.ToInt32(row["RoundId"]),
                    StartTime = Convert.ToDateTime(row["StartTime"]),
                    MatchNumber = JoeConvert.ToInt32(row["MatchNumber"]),
                    Field = row["Field"].ToString(),
                    TournamentLevel = row["TournamentLevel"].ToString()
                };

                JoeSqlParameterCollection parameters = new JoeSqlParameterCollection();
                parameters.Add("$RoundId", round.Id);
                using DataTable teamTable = await connection.GetDataTableAsync("SELECT B.TeamNumber, A.Station, " +
                                                                               "CASE coalesce(A.AutoMoved, A.AutoAmp, A.AutoSpeaker, A.Amp, A.Speaker, A.OnStage, " +
                                                                               "A.OnStageChain, A.NoteInOnChain, A.Spotlight, A.Notes, 'NN') WHEN 'NN' THEN 0 ELSE 1 END AS 'HasData' " +
                                                                               "FROM TeamRound A " +
                                                                               "INNER JOIN Team B ON A.TeamId = B.TeamId " +
                                                                               "WHERE A.RoundId = $RoundId;", parameters);

                foreach (DataRow teamRow in teamTable.Rows)
                {
                    round.TeamAssignments.Add(new TeamAssignment
                    {
                        TeamNumber = JoeConvert.ToInt32(teamRow["TeamNumber"]),
                        Station = teamRow["Station"].ToString(),
                        HasData = JoeConvert.ToBoolean(teamRow["HasData"])
                    });
                }

                rounds.Add(round);
            }

            return rounds;
        }

        public async Task SaveRoundsAsync(List<Round> rounds)
        {
            using JoeSqliteConnection connection = GetOpenConnection();

            JoeSqlParameterCollection parameters = new JoeSqlParameterCollection();
            foreach (Round round in rounds)
            {
                parameters.Clear();
                parameters.Add("$StartTime", round.StartTime);
                parameters.Add("$MatchNumber", round.MatchNumber);
                parameters.Add("$Field", round.Field);
                parameters.Add("$TournamentLevel", round.TournamentLevel);

                await connection.ExecuteNonQueryAsync("INSERT INTO Round(StartTime, MatchNumber, Field, TournamentLevel) " +
                                                      "VALUES ($StartTime, $MatchNumber, $Field, $TournamentLevel) ", parameters);

                int id = JoeConvert.ToInt32(await connection.ExecuteScalarAsync("SELECT last_insert_rowid()"));

                foreach (TeamAssignment teamAssignment in round.TeamAssignments)
                {
                    parameters.Clear();
                    parameters.Add("$RoundId", id);
                    parameters.Add("$TeamNumber", teamAssignment.TeamNumber);
                    parameters.Add("$Station", teamAssignment.Station);

                    await connection.ExecuteNonQueryAsync("INSERT INTO TeamRound(TeamId, RoundId, Station) " +
                                                          "SELECT A.TeamId, $RoundId, $Station " +
                                                          "FROM Team A " +
                                                          "WHERE A.TeamNumber = $TeamNumber;", parameters);
                }
            }
        }

        public async Task SaveTeamRoundAsync(TeamRound teamRound)
        {
            using JoeSqliteConnection connection = GetOpenConnection();

            JoeSqlParameterCollection parameters = new JoeSqlParameterCollection();

            parameters.Add("$MatchNumber", teamRound.MatchNumber);
            parameters.Add("$TeamNumber", teamRound.TeamNumber);

            using DataTable teamAndRoundIdTable = await connection.GetDataTableAsync("SELECT B.TeamId, C.RoundId " +
                                                                                     "FROM TeamRound A " +
                                                                                     "INNER JOIN Team B ON A.TeamId = B.TeamId " +
                                                                                     "INNER JOIN Round C ON A.RoundId = C.RoundId " +
                                                                                     "WHERE B.TeamNumber = $TeamNumber AND C.MatchNumber = $MatchNumber;", parameters);
            DataRow teamAndRoundIdRow = teamAndRoundIdTable.Rows[0];

            int roundId = JoeConvert.ToInt32(teamAndRoundIdRow["RoundId"]);
            int teamId = JoeConvert.ToInt32(teamAndRoundIdRow["TeamId"]);

            if (roundId == 0) throw new InvalidOperationException($"Round not found: {teamRound.MatchNumber}");

            parameters.Clear();
            parameters.Add("$RoundId", roundId);
            parameters.Add("$TeamId", teamId);

            parameters.Add("$AutoMoved", teamRound.AutoMoved);
            parameters.Add("$AutoAmp", teamRound.AutoAmp);
            parameters.Add("$AutoSpeaker", teamRound.AutoSpeaker);

            parameters.Add("$Amp", teamRound.Amp);
            parameters.Add("$Speaker", teamRound.Speaker);
            parameters.Add("$OnStage", teamRound.OnStage);
            parameters.Add("$OnStageChain", teamRound.OnStageChain);
            parameters.Add("$Spotlight", teamRound.Spotlight);

            parameters.Add("$Notes", teamRound.Notes);

            await connection.ExecuteNonQueryAsync("UPDATE TeamRound " +
                                                  "SET AutoMoved = $AutoMoved, " +
                                                  "AutoAmp = $AutoAmp, " +
                                                  "AutoSpeaker = $AutoSpeaker, " +
                                                  "Amp = $Amp, " +
                                                  "Speaker = $Speaker, " +
                                                  "OnStage = $OnStage, " +
                                                  "OnStageChain = $OnStageChain, " +
                                                  "Spotlight = $Spotlight, " +
                                                  "Notes = $Notes " +
                                                  "WHERE RoundId = $RoundId AND TeamId = $TeamId;", parameters);
        }

        public async Task<TeamRound> GetTeamRoundAsync(int matchNumber, int teamNumber)
        {
            using JoeSqliteConnection connection = GetOpenConnection();

            JoeSqlParameterCollection parameters = new JoeSqlParameterCollection();

            parameters.Add("$MatchNumber", matchNumber);
            parameters.Add("$TeamNumber", teamNumber);

            using DataTable roundTable = await connection.GetDataTableAsync("SELECT A.*, B.MatchNumber, C.TeamNumber " +
                                                                            "FROM TeamRound A " +
                                                                            "INNER JOIN Round B ON A.RoundId = B.RoundId " +
                                                                            "INNER JOIN Team C On A.TeamId = C.TeamId " +
                                                                            "WHERE B.MatchNumber = $MatchNumber AND C.TeamNumber = $TeamNumber; ", parameters);

            DataRow row = roundTable.Rows[0];

            return ConvertDataRowToTeamRound(row);
        }

        private static TeamRound ConvertDataRowToTeamRound(DataRow row)
        {
            return new TeamRound
            {
                TeamNumber = JoeConvert.ToInt32(row["TeamNumber"]),
                MatchNumber = JoeConvert.ToInt32(row["MatchNumber"]),
                AutoMoved = JoeConvert.ToBoolean(row["AutoMoved"]),
                AutoAmp = JoeConvert.ToInt32(row["AutoAmp"]),
                AutoSpeaker = JoeConvert.ToInt32(row["AutoSpeaker"]),
                Amp = JoeConvert.ToInt32(row["Amp"]),
                Speaker = JoeConvert.ToInt32(row["Speaker"]),
                OnStage = JoeConvert.ToBoolean(row["OnStage"]),
                OnStageChain = JoeConvert.ToBoolean(row["OnStageChain"]),
                NoteInOnChain = JoeConvert.ToBoolean(row["NoteInOnChain"]),
                Spotlight = JoeConvert.ToBoolean(row["Spotlight"]),
                Notes = JoeConvert.ToString(row["Comments"]),
                HasData = JoeConvert.ToBoolean(row["HasData"])
            };
        }

        private JoeSqliteConnection GetOpenConnection()
        {
            JoeSqliteConnection connection = new JoeSqliteConnection(GetConnectionString());
            connection.Open();
            return connection;
        }

        private string GetConnectionString()
        {
            string databaseFilePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");
            string connectionString = $"Data Source={databaseFilePath}";

            return connectionString;
        }
    }
}
