﻿using Joe.Common;
using Joe.Common.DataAccess;
using VScoutCentral.Models;

namespace VScoutCentral.Services
{
    internal class ScheduleDatabaseService : IScheduleDatabaseService
    {
        public async Task DeleteRoundsAsync()
        {
            using JoeSqliteConnection connection = GetOpenConnection();
            await connection.ExecuteNonQueryAsync("DELETE FROM Round; " +
                                                  "DELETE FROM TeamAssignment; ");
        }

        public async Task<List<Team>> GetTeams()
        {
            using JoeSqliteConnection connection = GetOpenConnection();

            using System.Data.DataTable roundTable = await connection.GetDataTableAsync("SELECT A.* " +
                                                                                        "FROM Team A; ");

            List<Team> teams = new List<Team>(roundTable.Rows.Count);

            foreach (System.Data.DataRow row in roundTable.Rows)
            {
                teams.Add(new Team
                {
                    Id = JoeConvert.ToInt32(row["TeamId"]),
                    Number = JoeConvert.ToInt32(row["TeamNumber"]),
                    Name = JoeConvert.ToString(row["TeamName"]),
                    Rank = JoeConvert.ToInt32(row["Rank"]),
                    MatchesPlayed = JoeConvert.ToInt32(row["MatchesPlayed"]),
                    Wins = JoeConvert.ToInt32(row["Wins"]),
                    Losses = JoeConvert.ToInt32(row["Losses"])
                });
            }

            return teams;
        }

        public async Task UpdateTeamsAsync(List<Team> teams)
        {
            using JoeSqliteConnection connection = GetOpenConnection();

            JoeSqlParameterCollection parameters = new JoeSqlParameterCollection();
            foreach (Team team in teams)
            {
                parameters.Clear();
                parameters.Add("$Id", team.Id);
                parameters.Add("$TeamNumber", team.Number);
                parameters.Add("$TeamName", team.Name);
                parameters.Add("$Rank", team.Rank);
                parameters.Add("$MatchesPlayed", team.MatchesPlayed);
                parameters.Add("$Wins", team.Wins);
                parameters.Add("$Losses", team.Losses);
                parameters.Add("$Ccwm", team.Ccwm);
                parameters.Add("$Dpr", team.Dpr);
                parameters.Add("$Opr", team.Opr);

                await connection.ExecuteNonQueryAsync("UPDATE Team " +
                                                      "SET TeamNumber = $TeamNumber, " +
                                                      "TeamName = $TeamName," +
                                                      "Rank = $Rank, " +
                                                      "MatchesPlayed = $MatchesPlayed," +
                                                      "Wins = $Wins," +
                                                      "Losses = $Losses," +
                                                      "Ccwm = $Ccwm," +
                                                      "Dpr = $Dpr," +
                                                      "Opr = $Opr " +
                                                      "WHERE TeamId = $Id; ", parameters);
            }
        }

        public async Task InsertTeamsAysnc(List<Team> teams)
        {
            using JoeSqliteConnection connection = GetOpenConnection();

            JoeSqlParameterCollection parameters = new JoeSqlParameterCollection();
            foreach (Team team in teams)
            {
                parameters.Clear();
                parameters.Add("$TeamNumber", team.Number);
                parameters.Add("$TeamName", team.Name);
                parameters.Add("$Rank", team.Rank);
                parameters.Add("$MatchesPlayed", team.MatchesPlayed);
                parameters.Add("$Wins", team.Wins);
                parameters.Add("$Losses", team.Losses);
                parameters.Add("$Ccwm", team.Ccwm);
                parameters.Add("$Dpr", team.Dpr);
                parameters.Add("$Opr", team.Opr);

                await connection.ExecuteNonQueryAsync("INSERT INTO Team(TeamNumber, TeamName, Rank, MatchesPlayed, Wins, Losses, Ccwm, Dpr, Opr) " +
                                                      "VALUES ($TeamNumber, $TeamName, $Rank, $MatchesPlayed, $Wins, $Losses, $Ccwm, $Dpr, $Opr); ", parameters);
            }
        }

        public async Task SaveTeamRoundAsync(TeamRound teamRound)
        {
            using JoeSqliteConnection connection = GetOpenConnection();

            JoeSqlParameterCollection parameters = new JoeSqlParameterCollection();

            parameters.Add("$MatchNumber", teamRound.MatchNumber);
            parameters.Add("$TeamNumber", teamRound.TeamNumber);

            using System.Data.DataTable teamAndRoundIdTable = await connection.GetDataTableAsync("SELECT B.TeamId, C.RoundId " +
                                                                                                 "FROM TeamRound A " +
                                                                                                 "INNER JOIN Team B ON A.TeamId = B.TeamId " +
                                                                                                 "INNER JOIN Round C ON A.RoundId = C.RoundId " +
                                                                                                 "WHERE B.TeamNumber = $TeamNumber AND C.MatchNumber = $MatchNumber;", parameters);
            System.Data.DataRow teamAndRoundIdRow = teamAndRoundIdTable.Rows[0];

            int roundId = JoeConvert.ToInt32(teamAndRoundIdRow["RoundId"]);
            int teamId = JoeConvert.ToInt32(teamAndRoundIdRow["TeamId"]);

            if (roundId == 0) throw new InvalidOperationException($"Round not found: {teamRound.MatchNumber}");

            parameters.Clear();
            parameters.Add("$RoundId", roundId);
            parameters.Add("$TeamId", teamId);

            parameters.Add("$AutonomousMoved", teamRound.AutoMoved);
            parameters.Add("$AutonomousDocked", teamRound.AutoDocked);
            parameters.Add("$AutonomousEngaged", teamRound.AutoEngaged);
            parameters.Add("$AutonomousConeHigh", teamRound.AutoConeHigh);
            parameters.Add("$AutonomousConeMiddle", teamRound.AutoConeMiddle);
            parameters.Add("$AutonomousConeLow", teamRound.AutoConeLow);
            parameters.Add("$AutonomousCubeHigh", teamRound.AutoCubeHigh);
            parameters.Add("$AutonomousCubeMiddle", teamRound.AutoCubeMiddle);
            parameters.Add("$AutonomousCubeLow", teamRound.AutoCubeLow);

            parameters.Add("$Docked", teamRound.Docked);
            parameters.Add("$Engaged", teamRound.Engaged);
            parameters.Add("$ConeHigh", teamRound.ConeHigh);
            parameters.Add("$ConeMiddle", teamRound.ConeMiddle);
            parameters.Add("$ConeLow", teamRound.ConeLow);
            parameters.Add("$CubeHigh", teamRound.CubeHigh);
            parameters.Add("$CubeMiddle", teamRound.CubeMiddle);
            parameters.Add("$CubeLow", teamRound.CubeLow);

            parameters.Add("$Notes", teamRound.Notes);

            await connection.ExecuteNonQueryAsync("UPDATE TeamRound " +
                                                  "SET AutonomousMoved = $AutonomousMoved, " +
                                                  "AutonomousConeHigh = $AutonomousConeHigh, " +
                                                  "AutonomousConeMiddle = $AutonomousConeMiddle, " +
                                                  "AutonomousConeBottom = $AutonomousConeLow, " +
                                                  "AutonomousCubeHigh = $AutonomousCubeHigh, " +
                                                  "AutonomousCubeMiddle = $AutonomousCubeMiddle, " +
                                                  "AutonomousCubeBottom = $AutonomousCubeLow, " +
                                                  "AutonomousDocked = $AutonomousDocked, " +
                                                  "AutonomousEngaged = $AutonomousEngaged, " +
                                                  "ConeHigh = $ConeHigh, " +
                                                  "ConeMiddle = $ConeMiddle, " +
                                                  "ConeBottom = $ConeLow, " +
                                                  "CubeHigh = $CubeHigh, " +
                                                  "CubeMiddle = $CubeMiddle, " +
                                                  "CubeBottom = $CubeLow, " +
                                                  "Docked = $Docked, " +
                                                  "Engaged = $Engaged, " +
                                                  "Comments = $Notes " +
                                                  "WHERE RoundId = $RoundId AND TeamId = $TeamId;", parameters);
        }

        public async Task<List<Round>> GetRoundsAsync()
        {
            using JoeSqliteConnection connection = GetOpenConnection();

            using System.Data.DataTable roundTable = await connection.GetDataTableAsync("SELECT A.* " +
                                                                                        "FROM Round A; ");

            List<Round> rounds = new List<Round>(roundTable.Rows.Count);

            foreach (System.Data.DataRow row in roundTable.Rows)
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
                using System.Data.DataTable teamTable = await connection.GetDataTableAsync("SELECT B.TeamNumber, A.Station, " +
                                                                                           "CASE coalesce(A.AutonomousMoved, A.AutonomousConeHigh, A.AutonomousConeMiddle, A.AutonomousConeBottom, A.AutonomousCubeHigh, A.AutonomousCubeMiddle, " +
                                                                                           "A.AutonomousCubeBottom, A.AutonomousDocked, A.AutonomousEngaged, A.ConeHigh, A.ConeMiddle, A.ConeBottom, A.CubeHigh, A.CubeMiddle, " +
                                                                                           "A.CubeBottom, A.Comments, A.Docked, A.Engaged, A.Parked, 'NN') WHEN 'NN' THEN 0 ELSE 1 END AS 'HasData' " +
                                                                                           "FROM TeamRound A " +
                                                                                           "INNER JOIN Team B ON A.TeamId = B.TeamId " +
                                                                                           "WHERE A.RoundId = $RoundId;", parameters);

                foreach (System.Data.DataRow teamRow in teamTable.Rows)
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

        public async Task<List<(int TeamNumber, string Station)>> GetTeamsInRoundAsync(int matchNumber)
        {
            using JoeSqliteConnection connection = GetOpenConnection();

            JoeSqlParameterCollection parameters = new JoeSqlParameterCollection();

            parameters.Add("$MatchNumber", matchNumber);

            List<(int TeamNumber, string Station)> teamNumbers = new List<(int TeamNumber, string Station)>();

            using System.Data.DataTable roundTable = await connection.GetDataTableAsync("SELECT B.TeamNumber, A.Station " +
                                                                                        "FROM TeamRound A " +
                                                                                        "INNER JOIN Team B ON B.TeamId = A.TeamId " +
                                                                                        "INNER JOIN Round C ON C.RoundId = A.RoundId " +
                                                                                        "WHERE C.MatchNumber = $MatchNumber; ", parameters);

            foreach (System.Data.DataRow row in roundTable.Rows)
            {
                teamNumbers.Add(new()
                {
                    TeamNumber = JoeConvert.ToInt32(row["TeamNumber"]),
                    Station = JoeConvert.ToString(row["Station"])
                });
            }

            return teamNumbers;
        }

        public async Task<List<TeamRound>> GetTeamRoundsAsync()
        {
            using JoeSqliteConnection connection = GetOpenConnection();

            JoeSqlParameterCollection parameters = new JoeSqlParameterCollection();

            using System.Data.DataTable roundTable = await connection.GetDataTableAsync("SELECT A.*, B.*, C.MatchNumber, " +
                                                                                        "CASE coalesce(A.AutonomousMoved, A.AutonomousConeHigh, A.AutonomousConeMiddle, A.AutonomousConeBottom, A.AutonomousCubeHigh, A.AutonomousCubeMiddle, " +
                                                                                        "A.AutonomousCubeBottom, A.AutonomousDocked, A.AutonomousEngaged, A.ConeHigh, A.ConeMiddle, A.ConeBottom, A.CubeHigh, A.CubeMiddle, " +
                                                                                        "A.CubeBottom, A.Comments, A.Docked, A.Engaged, A.Parked, 'NN') WHEN 'NN' THEN 0 ELSE 1 END AS 'HasData' " +
                                                                                        "FROM TeamRound A " +
                                                                                        "INNER JOIN Team B ON B.TeamId = A.TeamId " +
                                                                                        "INNER JOIN Round C ON C.RoundId = A.RoundId; ", parameters);

            List<TeamRound> teamRounds = new List<TeamRound>(roundTable.Rows.Count);

            foreach (System.Data.DataRow row in roundTable.Rows)
            {
                teamRounds.Add(new TeamRound
                {
                    TeamNumber = JoeConvert.ToInt32(row["TeamNumber"]),
                    MatchNumber = JoeConvert.ToInt32(row["MatchNumber"]),
                    Rank = JoeConvert.ToInt32(row["Rank"]),
                    Wins = JoeConvert.ToInt32(row["Wins"]),
                    Losses = JoeConvert.ToInt32(row["Losses"]),
                    Ccwm = JoeConvert.ToDecimal(row["Ccwm"]),
                    Dpr = JoeConvert.ToDecimal(row["Dpr"]),
                    Opr = JoeConvert.ToDecimal(row["Opr"]),
                    AutoMoved = JoeConvert.ToBoolean(row["AutonomousMoved"]),
                    AutoDocked = JoeConvert.ToBoolean(row["AutonomousDocked"]),
                    AutoEngaged = JoeConvert.ToBoolean(row["AutonomousEngaged"]),
                    AutoConeHigh = JoeConvert.ToInt32(row["AutonomousConeHigh"]),
                    AutoConeMiddle = JoeConvert.ToInt32(row["AutonomousConeMiddle"]),
                    AutoConeLow = JoeConvert.ToInt32(row["AutonomousConeBottom"]),
                    AutoCubeHigh = JoeConvert.ToInt32(row["AutonomousCubeHigh"]),
                    AutoCubeMiddle = JoeConvert.ToInt32(row["AutonomousCubeMiddle"]),
                    AutoCubeLow = JoeConvert.ToInt32(row["AutonomousCubeBottom"]),
                    ConeHigh = JoeConvert.ToInt32(row["ConeHigh"]),
                    ConeMiddle = JoeConvert.ToInt32(row["ConeMiddle"]),
                    ConeLow = JoeConvert.ToInt32(row["ConeBottom"]),
                    CubeHigh = JoeConvert.ToInt32(row["CubeHigh"]),
                    CubeMiddle = JoeConvert.ToInt32(row["CubeMiddle"]),
                    CubeLow = JoeConvert.ToInt32(row["CubeBottom"]),
                    Docked = JoeConvert.ToBoolean(row["Docked"]),
                    Engaged = JoeConvert.ToBoolean(row["Engaged"]),
                    Notes = JoeConvert.ToString(row["Comments"]),
                    HasData = JoeConvert.ToBoolean(row["HasData"])
                });
            }

            return teamRounds;
        }

        public async Task<List<TeamRound>> GetTeamRoundsAsync(int teamNumber)
        {
            using JoeSqliteConnection connection = GetOpenConnection();

            JoeSqlParameterCollection parameters = new JoeSqlParameterCollection();

            parameters.Add("$TeamNumber", teamNumber);

            using System.Data.DataTable roundTable = await connection.GetDataTableAsync("SELECT A.*, B.*, C.MatchNumber, " +
                                                                                        "CASE coalesce(A.AutonomousMoved, A.AutonomousConeHigh, A.AutonomousConeMiddle, A.AutonomousConeBottom, A.AutonomousCubeHigh, A.AutonomousCubeMiddle, " +
                                                                                        "A.AutonomousCubeBottom, A.AutonomousDocked, A.AutonomousEngaged, A.ConeHigh, A.ConeMiddle, A.ConeBottom, A.CubeHigh, A.CubeMiddle, " +
                                                                                        "A.CubeBottom, A.Comments, A.Docked, A.Engaged, A.Parked, 'NN') WHEN 'NN' THEN 0 ELSE 1 END AS 'HasData' " +
                                                                                        "FROM TeamRound A " +
                                                                                        "INNER JOIN Team B ON B.TeamId = A.TeamId " +
                                                                                        "INNER JOIN Round C ON C.RoundId = A.RoundId " +
                                                                                        "WHERE B.TeamNumber = $TeamNumber; ", parameters);

            List<TeamRound> teamRounds = new List<TeamRound>(roundTable.Rows.Count);

            foreach (System.Data.DataRow row in roundTable.Rows)
            {
                teamRounds.Add(new TeamRound
                {
                    TeamNumber = JoeConvert.ToInt32(row["TeamNumber"]),
                    MatchNumber = JoeConvert.ToInt32(row["MatchNumber"]),
                    Rank = JoeConvert.ToInt32(row["Rank"]),
                    Wins = JoeConvert.ToInt32(row["Wins"]),
                    Losses = JoeConvert.ToInt32(row["Losses"]),
                    AutoMoved = JoeConvert.ToBoolean(row["AutonomousMoved"]),
                    AutoDocked = JoeConvert.ToBoolean(row["AutonomousDocked"]),
                    AutoEngaged = JoeConvert.ToBoolean(row["AutonomousEngaged"]),
                    AutoConeHigh = JoeConvert.ToInt32(row["AutonomousConeHigh"]),
                    AutoConeMiddle = JoeConvert.ToInt32(row["AutonomousConeMiddle"]),
                    AutoConeLow = JoeConvert.ToInt32(row["AutonomousConeBottom"]),
                    AutoCubeHigh = JoeConvert.ToInt32(row["AutonomousCubeHigh"]),
                    AutoCubeMiddle = JoeConvert.ToInt32(row["AutonomousCubeMiddle"]),
                    AutoCubeLow = JoeConvert.ToInt32(row["AutonomousCubeBottom"]),
                    ConeHigh = JoeConvert.ToInt32(row["ConeHigh"]),
                    ConeMiddle = JoeConvert.ToInt32(row["ConeMiddle"]),
                    ConeLow = JoeConvert.ToInt32(row["ConeBottom"]),
                    CubeHigh = JoeConvert.ToInt32(row["CubeHigh"]),
                    CubeMiddle = JoeConvert.ToInt32(row["CubeMiddle"]),
                    CubeLow = JoeConvert.ToInt32(row["CubeBottom"]),
                    Docked = JoeConvert.ToBoolean(row["Docked"]),
                    Engaged = JoeConvert.ToBoolean(row["Engaged"]),
                    Notes = JoeConvert.ToString(row["Comments"]),
                    HasData = JoeConvert.ToBoolean(row["HasData"])
                });
            }

            return teamRounds;
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

                int id = Convert.ToInt32(await connection.ExecuteScalarAsync("SELECT last_insert_rowid()"));

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
