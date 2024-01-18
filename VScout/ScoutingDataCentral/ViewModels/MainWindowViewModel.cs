using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using VScoutCommon.Common;
using VScoutCommon.Models;
using VScoutCommon.Models.BusinessModels;
using VScoutCommon.ViewModels;

namespace ScoutingDataCentral.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private string status;
        public string Status
        {
            get
            {
                return this.status;
            }

            set
            {
                this.status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public void CreateExportAllReport()
        {
            string filePath = DialogCommon.GetSaveFilePathFromUser(FileType.Csv);
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                List<TeamFileRecord2022> teamFileRecords = TeamFile2022.GetAll(Constants.CentralBaseFilePath);

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("TeamNumber,Name,School,City,State,RookieYear,Round,AutoMove,AutoLow,AutoHigh,TeleLow,TeleHigh,LowRung,SecondRung,ThirdRung,HighRung,Notes,Opr");

                foreach (TeamFileRecord2022 teamFileRecord in teamFileRecords)
                {
                    foreach (TeamFileRecord2022.Round round in teamFileRecord.Rounds)
                    {
                        string autoMoved = TeamFile.ConvertBoolToString(round.Autonomous.Moved);
                        string autoLow = round.Autonomous.Hub.LowGoal.ToString();
                        string autoHigh = round.Autonomous.Hub.HighGoal.ToString();
                        string teleLow = round.Hub.LowGoal.ToString();
                        string teleHigh = round.Hub.HighGoal.ToString();
                        string lowRung = TeamFile.ConvertBoolToString(round.Endgame.LowRung);
                        string secondRung = TeamFile.ConvertBoolToString(round.Endgame.SecondRung);
                        string thirdRung = TeamFile.ConvertBoolToString(round.Endgame.ThirdRung);
                        string highRung = TeamFile.ConvertBoolToString(round.Endgame.HighRung);

                        stringBuilder.AppendLine($"{teamFileRecord.TeamNumber},{teamFileRecord.Name},{teamFileRecord.SchoolName},{teamFileRecord.City}," +
                            $"{teamFileRecord.State},{teamFileRecord.RookieYear},{round.Number},{autoMoved},{autoLow},{autoHigh}," +
                            $"{teleLow},{teleHigh},{lowRung},{secondRung},{thirdRung},{highRung},{round.Comments.Replace("\n", ";").Replace(",", ".")},{teamFileRecord.Opr}");
                    }
                }

                File.WriteAllText(filePath, stringBuilder.ToString());

                this.Status = $"Successfully exported all data to CSV ({filePath}).";
            }
        }

        public void CreateTeamScheduleExport()
        {
            // Round, Team, Color, Position
            string filePath = DialogCommon.GetSaveFilePathFromUser(FileType.Csv);
            string schedulePath = Path.Combine(Constants.CentralBaseFilePath, "Schedule.json");
            Schedule schedule = JsonConvert.DeserializeObject<Schedule>(File.ReadAllText(schedulePath));

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Round,Team,Color,Position");

            foreach (Round round in schedule.Rounds)
            {
                TeamAssignment red1 = round.Teams.First(t => t.Station == "Red1");
                TeamAssignment red2 = round.Teams.First(t => t.Station == "Red2");
                TeamAssignment red3 = round.Teams.First(t => t.Station == "Red3");
                TeamAssignment blue1 = round.Teams.First(t => t.Station == "Blue1");
                TeamAssignment blue2 = round.Teams.First(t => t.Station == "Blue2");
                TeamAssignment blue3 = round.Teams.First(t => t.Station == "Blue3");
                stringBuilder.AppendLine($"{round.MatchNumber},{red1.TeamNumber},red,1");
                stringBuilder.AppendLine($"{round.MatchNumber},{red2.TeamNumber},red,2");
                stringBuilder.AppendLine($"{round.MatchNumber},{red3.TeamNumber},red,3");
                stringBuilder.AppendLine($"{round.MatchNumber},{blue1.TeamNumber},blue,1");
                stringBuilder.AppendLine($"{round.MatchNumber},{blue2.TeamNumber},blue,2");
                stringBuilder.AppendLine($"{round.MatchNumber},{blue3.TeamNumber},blue,3");
            }

            File.WriteAllText(filePath, stringBuilder.ToString());

            this.Status = $"Successfully exported team schedule ({filePath}).";
        }

        public void CreateScheduleExport()
        {
            string filePath = DialogCommon.GetSaveFilePathFromUser(FileType.Csv);
            string schedulePath = Path.Combine(Constants.CentralBaseFilePath, "Schedule.json");
            Schedule schedule = JsonConvert.DeserializeObject<Schedule>(File.ReadAllText(schedulePath));

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("MatchNumber,Time,Red1,Red2,Red3,Blue1,Blue2,Blue3");

            foreach (Round round in schedule.Rounds)
            {
                int red1 = round.Teams.First(t => t.Station == "Red1").TeamNumber;
                int red2 = round.Teams.First(t => t.Station == "Red2").TeamNumber;
                int red3 = round.Teams.First(t => t.Station == "Red3").TeamNumber;
                int blue1 = round.Teams.First(t => t.Station == "Blue1").TeamNumber;
                int blue2 = round.Teams.First(t => t.Station == "Blue2").TeamNumber;
                int blue3 = round.Teams.First(t => t.Station == "Blue3").TeamNumber;
                stringBuilder.AppendLine($"{round.MatchNumber},{round.StartTime},{red1},{red2},{red3},{blue1},{blue2},{blue3}");
            }

            File.WriteAllText(filePath, stringBuilder.ToString());

            this.Status = $"Successfully exported schedule ({filePath}).";
        }

        public void SaveSchedule()
        {
            string filePath = DialogCommon.GetSaveFilePathFromUser(FileType.Json);
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                using (FRCClient frcClient = new FRCClient(Constants.EventCode, int.Parse(Constants.Year)))
                {
                    string scheduleFileContents = frcClient.GetScheduleFileContents();
                    File.WriteAllText(filePath, scheduleFileContents);
                }

                this.Status = "Successfully downloaded schedule.";
            }
        }

        public void SyncWithFRCData()
        {
            this.Status = "Syncing with FRC data...";
            int year = int.Parse(Constants.Year);

            using (FRCClient frcClient = new FRCClient(Constants.EventCode, year))
            using (BlueAllianceClient blueAllianceClient = new BlueAllianceClient(Constants.EventCode, year))
            {
                string temp = blueAllianceClient.GetOprsString();
                JToken jroot = null;
                if (temp.Length > 10)
                {
                    jroot = JToken.Parse(temp);
                }

                List<VScoutCommon.Models.Team> teams = frcClient.GetTeams();
                List<VScoutCommon.Models.Ranking> rankings = frcClient.GetRankings();
                foreach (VScoutCommon.Models.Team team in teams)
                {
                    decimal? opr = null;
                    if (jroot != null)
                    {
                        JToken objectOpr = jroot["oprs"][$"frc{team.TeamNumber}"];
                        if (objectOpr != null)
                        {
                            opr = objectOpr.ToObject<decimal>();
                        }
                    }

                    string teamDirectory = Path.Combine(Constants.CentralBaseFilePath, team.TeamNumber.ToString());
                    Directory.CreateDirectory(teamDirectory);

                    int? rank = rankings.FirstOrDefault(m => m.TeamNumber == team.TeamNumber)?.Rank;
                    string teamFile = Path.Combine(teamDirectory, team.TeamNumber.ToString() + ".xml");
                    if (!File.Exists(teamFile))
                    {
                        CreateTeamFile(teamFile, team, rank);
                    }
                    else if (rank.HasValue || opr.HasValue)
                    {
                        XElement root = XElement.Parse(File.ReadAllText(teamFile));
                        if (rank.HasValue)
                        {
                            root.SetAttributeValue("rank", rank.Value);
                        }

                        if (opr.HasValue)
                        {
                            root.SetAttributeValue("opr", opr.Value);
                        }

                        root.Save(teamFile);
                    }
                }
            }

            this.Status = "Successfully synced with FRC data.";
        }

        private void CreateTeamFile(string filePath, VScoutCommon.Models.Team team, int? rank)
        {
            XElement root = new XElement("Team");
            root.SetAttributeValue("number", team.TeamNumber);
            root.SetAttributeValue("name", team.NameShort);
            root.SetAttributeValue("schoolName", team.SchoolName);
            root.SetAttributeValue("city", team.City);
            root.SetAttributeValue("state", team.StateProv);
            root.SetAttributeValue("rookieYear", team.RookieYear);
            if (rank.HasValue)
            {
                root.SetAttributeValue("rank", rank.Value);
            }

            root.Save(filePath);
        }

        public void ImportImages()
        {
            string folderPath = DialogCommon.GetFolderPathFromUser();

            if (folderPath != null)
            {
                string[] imagesToDownload = Directory.GetFiles(folderPath, "*.jpg", SearchOption.AllDirectories);

                foreach (string image in imagesToDownload)
                {
                    if (int.TryParse(Path.GetFileNameWithoutExtension(image), out int teamNumber))
                    {
                        string destinationPath = TeamFile.GetTeamImagePath(Constants.CentralBaseFilePath, teamNumber);

                        Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));

                        File.Copy(image, destinationPath, true);
                    }
                }
            }
        }

        public void SyncFiles()
        {
            string folderPath = DialogCommon.GetFolderPathFromUser();

            if (folderPath != null)
            {
                string red1Path = Path.Combine(folderPath, "red1");
                SyncFolder(red1Path);

                string red2Path = Path.Combine(folderPath, "red2");
                SyncFolder(red2Path);

                string red3Path = Path.Combine(folderPath, "red3");
                SyncFolder(red3Path);

                string blue1Path = Path.Combine(folderPath, "blue1");
                SyncFolder(blue1Path);

                string blue2Path = Path.Combine(folderPath, "blue2");
                SyncFolder(blue2Path);

                string blue3Path = Path.Combine(folderPath, "blue3");
                SyncFolder(blue3Path);

                this.Status = "Done";
            }
        }

        private void SyncFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath)) return;

            string[] filesToImport = Directory.GetFiles(folderPath, "*.xml", SearchOption.AllDirectories);

            foreach (string fileToImport in filesToImport)
            {
                int teamNumber = int.Parse(Path.GetFileNameWithoutExtension(fileToImport));
                this.Status = $"Syncing team: {teamNumber}";
                string centralTeamFile = TeamFile.GetTeamFilePath(Constants.CentralBaseFilePath, teamNumber);

                if (!File.Exists(centralTeamFile))
                {
                    string centralDirectory = Path.GetDirectoryName(centralTeamFile);
                    if (!Directory.Exists(centralDirectory))
                    {
                        Directory.CreateDirectory(centralDirectory);
                    }
                    File.Copy(fileToImport, centralTeamFile);
                    continue;
                }

                XElement rootCentral = XElement.Parse(File.ReadAllText(centralTeamFile));
                XElement rootImport = XElement.Parse(File.ReadAllText(fileToImport));

                foreach (XElement round in rootImport.Elements("Round"))
                {
                    int importRoundNumber = (int)round.Attribute("number");
                    XElement centralRound = rootCentral.Elements("Round").FirstOrDefault(e => (int)e.Attribute("number") == importRoundNumber);
                    centralRound?.Remove();

                    rootCentral.Add(round);
                }

                rootCentral.Save(centralTeamFile);
            }
        }
    }
}
