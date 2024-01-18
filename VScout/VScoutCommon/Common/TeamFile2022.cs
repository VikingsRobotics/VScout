using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using VScoutCommon.Models.BusinessModels;

namespace VScoutCommon.Common
{
    public static class TeamFile2022
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

        public static List<TeamFileRecord2022> GetAll(string rootPath)
        {
            List<int> teamNumbers = GetAllTeamNumbers(rootPath);

            List<TeamFileRecord2022> teamFileRecords = new List<TeamFileRecord2022>(teamNumbers.Count);

            foreach (int teamNumber in teamNumbers)
            {
                teamFileRecords.Add(Get(rootPath, teamNumber));
            }

            return teamFileRecords;
        }

        public static TeamFileRecord2022 Get(string rootPath, int teamNumber)
        {
            string filePath = GetTeamFilePath(rootPath, teamNumber);

            if (!File.Exists(filePath)) return null;

            return Parse(File.ReadAllText(filePath));
        }

        public static void Save(string rootPath, TeamFileRecord2022 teamFileRecord)
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

            foreach (TeamFileRecord2022.Round round in teamFileRecord.Rounds)
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

                XElement autonomous = new XElement("Autonomous");
                autonomous.SetAttributeValue("moved", ConvertBoolToString(round.Autonomous.Moved));
                autonomous.SetAttributeValue("lowGoal", round.Autonomous.Hub.LowGoal);
                autonomous.SetAttributeValue("highGoal", round.Autonomous.Hub.HighGoal);
                roundElement.Add(autonomous);

                XElement powerPort = new XElement("Hub");
                powerPort.SetAttributeValue("lowGoal", round.Hub.LowGoal);
                powerPort.SetAttributeValue("highGoal", round.Hub.HighGoal);
                roundElement.Add(powerPort);

                XElement endgame = new XElement("Endgame");
                endgame.SetAttributeValue("lowBar", ConvertBoolToString(round.Endgame.LowRung));
                endgame.SetAttributeValue("secondBar", ConvertBoolToString(round.Endgame.SecondRung));
                endgame.SetAttributeValue("thirdBar", ConvertBoolToString(round.Endgame.ThirdRung));
                endgame.SetAttributeValue("highBar", ConvertBoolToString(round.Endgame.HighRung));
                roundElement.Add(endgame);

                XElement comments = new XElement("Comments")
                {
                    Value = round.Comments
                };
                roundElement.Add(comments);

                root.Save(filePath);
            }
        }

        public static TeamFileRecord2022 Parse(string fileContents)
        {
            TeamFileRecord2022 teamFileRecord = new TeamFileRecord2022();
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
                TeamFileRecord2022.Round round = new TeamFileRecord2022.Round();
                round.Number = (int)roundElement.Attribute("number");
                round.Comments = roundElement.Element("Comments").Value;

                XElement autonomous = roundElement.Element("Autonomous");
                round.Autonomous.Moved = ConvertStringToBool((string)autonomous.Attribute("moved"));
                round.Autonomous.Hub.LowGoal = (int)autonomous.Attribute("lowGoal");
                round.Autonomous.Hub.HighGoal = (int)autonomous.Attribute("highGoal");

                XElement powerPort = roundElement.Element("Hub");
                round.Hub.LowGoal = (int)powerPort.Attribute("lowGoal");
                round.Hub.HighGoal = (int)powerPort.Attribute("highGoal");

                XElement endgame = roundElement.Element("Endgame");
                round.Endgame.LowRung = ConvertStringToBool((string)endgame.Attribute("lowBar"));
                round.Endgame.SecondRung = ConvertStringToBool((string)endgame.Attribute("secondBar"));
                round.Endgame.ThirdRung = ConvertStringToBool((string)endgame.Attribute("thirdBar"));
                round.Endgame.HighRung = ConvertStringToBool((string)endgame.Attribute("highBar"));

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
