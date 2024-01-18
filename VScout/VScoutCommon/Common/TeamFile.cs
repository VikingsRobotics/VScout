using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using VScoutCommon.Models.BusinessModels;

namespace VScoutCommon.Common
{
    public static class TeamFile
    {
        public static string GetTeamImagePath(string rootPath, int teamNumber)
        {
            return Path.Combine(GetTeamDirectory(rootPath, teamNumber), $"{teamNumber}.jpg");
        }

        public static string GetTeamFilePath(string rootPath, int teamNumber)
        {
            return Path.Combine(GetTeamDirectory(rootPath, teamNumber), $"{teamNumber}.xml");
        }

        public static string GetTeamDirectory(string rootPath, int teamNumber)
        {
            return Path.Combine(rootPath, teamNumber.ToString());
        }

        public static List<int> GetAllTeamNumbers(string rootPath)
        {
            List<int> teamNumbers = new List<int>();
            foreach (string teamDirectory in Directory.EnumerateDirectories(rootPath))
            {
                if(int.TryParse(Path.GetFileName(teamDirectory), out int teamNumber))
                {
                    teamNumbers.Add(teamNumber);
                }
            }

            return teamNumbers;
        }

        public static List<TeamFileRecord2020> GetAll(string rootPath)
        {
            List<int> teamNumbers = GetAllTeamNumbers(rootPath);
            
            List<TeamFileRecord2020> teamFileRecords = new List<TeamFileRecord2020>(teamNumbers.Count);

            foreach (int teamNumber in teamNumbers)
            {
                teamFileRecords.Add(Get(rootPath, teamNumber));
            }

            return teamFileRecords;
        }

        public static TeamFileRecord2020 Get(string rootPath, int teamNumber)
        {
            string filePath = GetTeamFilePath(rootPath, teamNumber);

            if (!File.Exists(filePath)) return null;

            return Parse(File.ReadAllText(filePath));
        }

        public static void Save(string rootPath, TeamFileRecord2020 teamFileRecord)
        {
            if (teamFileRecord == null)
            {
                throw new System.ArgumentNullException(nameof(teamFileRecord));
            }

            string filePath = GetTeamFilePath(rootPath, teamFileRecord.TeamNumber);

            XElement root;
            if (!File.Exists(filePath))
            {
                string teamDirectory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(teamDirectory))
                {
                    Directory.CreateDirectory(teamDirectory);
                }

                root = new XElement("Team");
                root.SetAttributeValue("number", teamFileRecord.TeamNumber);
            }
            else
            {
                root = XElement.Parse(File.ReadAllText(filePath));
            }

            foreach (TeamFileRecord2020.Round round in teamFileRecord.Rounds)
            {
                XElement roundElement = root.Elements("Round").FirstOrDefault(element => (int)element.Attribute("number") == round.Number);
                if (roundElement == null)
                {
                    roundElement = new XElement("Round");
                    roundElement.SetAttributeValue("number", round.Number);
                    root.Add(roundElement);
                }
                else
                {
                    roundElement.RemoveNodes();
                }

                XElement underTurntable = new XElement("UnderTurntable");
                underTurntable.Value = ConvertBoolToString(round.GoUnderTurntable);
                roundElement.Add(underTurntable);

                XElement throughCity = new XElement("ThroughCity");
                throughCity.Value = ConvertBoolToString(round.GoThroughCity);
                roundElement.Add(throughCity);

                XElement autonomous = new XElement("Autonomous");
                autonomous.SetAttributeValue("moved", ConvertBoolToString(round.Autonomous.Moved));
                autonomous.SetAttributeValue("lowGoal", round.Autonomous.PowerPort.LowGoal);
                autonomous.SetAttributeValue("highGoal", round.Autonomous.PowerPort.HighGoal);
                roundElement.Add(autonomous);

                XElement powerPort = new XElement("PowerPort");
                powerPort.SetAttributeValue("lowGoal", round.PowerPort.LowGoal);
                powerPort.SetAttributeValue("highGoal", round.PowerPort.HighGoal);
                roundElement.Add(powerPort);

                XElement controlPanel = new XElement("ControlPanel");
                controlPanel.SetAttributeValue("turns", ConvertBoolToString(round.ControlPanel.AchievedRotationControl));
                controlPanel.SetAttributeValue("color", ConvertBoolToString(round.ControlPanel.AchievedPositionControl));
                roundElement.Add(controlPanel);

                XElement endgame = new XElement("Endgame");
                endgame.SetAttributeValue("balance", ConvertBoolToString(round.Endgame.AchievedBalance));
                endgame.SetAttributeValue("climb", ConvertBoolToString(round.Endgame.AchievedClimb));
                endgame.SetAttributeValue("getBackDown", ConvertBoolToString(round.Endgame.GetBackDown));
                roundElement.Add(endgame);

                XElement comments = new XElement("Comments")
                {
                    Value = round.Comments
                };
                roundElement.Add(comments);

                root.Save(filePath);
            }
        }

        public static TeamFileRecord2020 Parse(string fileContents)
        {
            TeamFileRecord2020 teamFileRecord = new TeamFileRecord2020();
            XElement root = XElement.Parse(fileContents);

            teamFileRecord.TeamNumber = (int)root.Attribute("number");

            XAttribute name = root.Attribute("name");
            if (name != null)
            {
                teamFileRecord.Name = (string)name;
            }

            XAttribute schoolName = root.Attribute("schoolName");
            if (schoolName != null)
            {
                teamFileRecord.SchoolName = (string)schoolName;
            }

            XAttribute city = root.Attribute("city");
            if (city != null)
            {
                teamFileRecord.City = (string)city;
            }

            XAttribute state = root.Attribute("state");
            if (state != null)
            {
                teamFileRecord.State = (string)state;
            }

            XAttribute rookieYear = root.Attribute("rookieYear");
            if (rookieYear != null)
            {
                teamFileRecord.RookieYear = (int)rookieYear;
            }

            XAttribute opr = root.Attribute("opr");
            if (opr != null)
            {
                teamFileRecord.Opr = (decimal)opr;
            }

            XAttribute rank = root.Attribute("rank");
            if (rank != null)
            {
                teamFileRecord.Rank = (int)rank;
            }

            foreach (XElement roundElement in root.Elements("Round"))
            {
                TeamFileRecord2020.Round round = new TeamFileRecord2020.Round();
                round.Number = (int)roundElement.Attribute("number");
                round.Comments = roundElement.Element("Comments").Value;
                round.GoUnderTurntable = ConvertStringToBool((string)roundElement.Element("UnderTurntable"));
                round.GoThroughCity = ConvertStringToBool((string)roundElement.Element("ThroughCity"));

                XElement autonomous = roundElement.Element("Autonomous");
                round.Autonomous.Moved = ConvertStringToBool((string)autonomous.Attribute("moved"));
                round.Autonomous.PowerPort.LowGoal = (int)autonomous.Attribute("lowGoal");
                round.Autonomous.PowerPort.HighGoal = (int)autonomous.Attribute("highGoal");

                XElement powerPort = roundElement.Element("PowerPort");
                round.PowerPort.LowGoal = (int)powerPort.Attribute("lowGoal");
                round.PowerPort.HighGoal = (int)powerPort.Attribute("highGoal");

                XElement controlPanel = roundElement.Element("ControlPanel");
                round.ControlPanel.AchievedRotationControl = ConvertStringToBool((string)controlPanel.Attribute("turns"));
                round.ControlPanel.AchievedPositionControl = ConvertStringToBool((string)controlPanel.Attribute("color"));

                XElement endgame = roundElement.Element("Endgame");
                round.Endgame.AchievedBalance = ConvertStringToBool((string)endgame.Attribute("balance"));
                round.Endgame.AchievedClimb = ConvertStringToBool((string)endgame.Attribute("climb"));
                round.Endgame.GetBackDown = ConvertStringToBool((string)endgame.Attribute("getBackDown"));

                teamFileRecord.Rounds.Add(round);
            }
            
            return teamFileRecord;
        }

        public static string ConvertBoolToString(bool value)
        {
            return value ? "Y" : "N";
        }

        public static bool ConvertStringToBool(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            
            return value.ToUpper() == "Y";
        }
    }
}
