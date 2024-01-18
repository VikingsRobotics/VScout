using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Media;
using VScoutCommon.Common;
using VScoutCommon.Models;
using VScoutCommon.Models.BusinessModels;
using VScoutCommon.ViewModels;

namespace VScoutDataCollector.ViewModels
{
    public class ScheduleViewModel : BaseViewModel
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

        private string station;
        public string Station
        {
            get
            {
                return this.station;
            }
            set
            {
                this.station = value;
                OnPropertyChanged(nameof(Station));
            }
        }

        private ObservableCollection<RoundViewModel> schedule = new ObservableCollection<RoundViewModel>();
        public ObservableCollection<RoundViewModel> Schedule
        {
            get
            {
                return this.schedule;
            }

            set
            {
                this.schedule = value;
                OnPropertyChanged(nameof(Schedule));
            }
        }

        private readonly Brush lightRedBrush = new SolidColorBrush(Color.FromRgb(255, 175, 175));
        private readonly Brush lightBlueBrush = new SolidColorBrush(Color.FromRgb(163, 198, 241));
        private readonly Brush darkRedBrush = new SolidColorBrush(Color.FromRgb(255, 75, 75));
        private readonly Brush darkBlueBrush = new SolidColorBrush(Color.FromRgb(47, 126, 223));
        private readonly Brush lightGrayBrush = new SolidColorBrush(Color.FromRgb(230, 230, 230));

        public void ExportData()
        {
            string externalDrive = DialogCommon.GetFolderPathFromUser();
            if (externalDrive != null)
            {
                Config config = ConfigParser.GetConfig();
                string destinationPath = Path.Combine(externalDrive, config.Station);
                DirectoryCopy(Constants.CollectorBaseFilePath, destinationPath, true);
                this.Status = "Done.";
            }
        }

        /// <summary>
        /// This is from Microsoft's website.
        /// </summary>
        /// <param name="sourceDirName"></param>
        /// <param name="destDirName"></param>
        /// <param name="copySubDirs"></param>
        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, true);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        public void ImportImages()
        {
            string folderPath = DialogCommon.GetFolderPathFromUser();

            if (folderPath != null)
            {
                string[] imagesToDownload = Directory.GetFiles(folderPath, "*.jpg", SearchOption.AllDirectories);

                foreach (string image in imagesToDownload)
                {
                    int teamNumber = int.Parse(Path.GetFileNameWithoutExtension(image));

                    string destinationPath = TeamFile.GetTeamImagePath(Constants.CollectorBaseFilePath, teamNumber);

                    Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));

                    File.Copy(image, destinationPath, true);
                }
            }
        }

        public void UpdateHasData(int teamNumber, int roundNumber)
        {
            RoundViewModel roundViewModel = this.Schedule.FirstOrDefault(r => r.MatchNumber == roundNumber);
            if (roundViewModel == null) return;

            if (roundViewModel.Red1TeamNumber == teamNumber)
            {
                roundViewModel.Red1BackColor = this.darkRedBrush;
            }
            else if (roundViewModel.Red2TeamNumber == teamNumber)
            {
                roundViewModel.Red2BackColor = this.darkRedBrush;
            }
            else if (roundViewModel.Red3TeamNumber == teamNumber)
            {
                roundViewModel.Red3BackColor = this.darkRedBrush;
            }
            else if (roundViewModel.Blue1TeamNumber == teamNumber)
            {
                roundViewModel.Blue1BackColor = this.darkBlueBrush;
            }
            else if (roundViewModel.Blue2TeamNumber == teamNumber)
            {
                roundViewModel.Blue2BackColor = this.darkBlueBrush;
            }
            else if (roundViewModel.Blue3TeamNumber == teamNumber)
            {
                roundViewModel.Blue3BackColor = this.darkBlueBrush;
            }
        }

        public void OpenFile()
        {
            string scheduleFilePath = Path.Combine(Constants.CollectorBaseFilePath, "Schedule.json");

            if (!File.Exists(scheduleFilePath))
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "JSON Files (.json)|*.json";

                bool? result = openFileDialog.ShowDialog();
                if (result != true) return;
                scheduleFilePath = openFileDialog.FileName;
            }

            string fileContents = File.ReadAllText(scheduleFilePath);

            Schedule schedule = JsonConvert.DeserializeObject<Schedule>(fileContents);
            Config config = ConfigParser.GetConfig();

            Brush red1Brush = this.lightGrayBrush;
            Brush red2Brush = this.lightGrayBrush;
            Brush red3Brush = this.lightGrayBrush;
            Brush blue1Brush = this.lightGrayBrush;
            Brush blue2Brush = this.lightGrayBrush;
            Brush blue3Brush = this.lightGrayBrush;

            switch (config.Station.ToLower())
            {
                case "red1":
                    red1Brush = lightRedBrush;
                    break;
                case "red2":
                    red2Brush = lightRedBrush;
                    break;
                case "red3":
                    red3Brush = lightRedBrush;
                    break;
                case "blue1":
                    blue1Brush = lightBlueBrush;
                    break;
                case "blue2":
                    blue2Brush = lightBlueBrush;
                    break;
                case "blue3":
                    blue3Brush = lightBlueBrush;
                    break;
                default:
                    throw new InvalidOperationException($"Unrecognized stations: {config.Station.ToLower()}");
            }

            List<TeamFileRecord2022> teamFileRecords = TeamFile2022.GetAll(Constants.CollectorBaseFilePath);

            foreach (Round round in schedule.Rounds)
            {
                RoundViewModel roundViewModel = new RoundViewModel
                {
                    MatchNumber = round.MatchNumber,
                    Red1TeamNumber = round.Teams.First(team => team.Station == "Red1").TeamNumber,
                    Red2TeamNumber = round.Teams.First(team => team.Station == "Red2").TeamNumber,
                    Red3TeamNumber = round.Teams.First(team => team.Station == "Red3").TeamNumber,
                    Blue1TeamNumber = round.Teams.First(team => team.Station == "Blue1").TeamNumber,
                    Blue2TeamNumber = round.Teams.First(team => team.Station == "Blue2").TeamNumber,
                    Blue3TeamNumber = round.Teams.First(team => team.Station == "Blue3").TeamNumber
                };

                bool hasRed1Data = HaveMatchData(teamFileRecords, roundViewModel.Red1TeamNumber, round.MatchNumber);
                bool hasRed2Data = HaveMatchData(teamFileRecords, roundViewModel.Red2TeamNumber, round.MatchNumber);
                bool hasRed3Data = HaveMatchData(teamFileRecords, roundViewModel.Red3TeamNumber, round.MatchNumber);
                bool hasBlue1Data = HaveMatchData(teamFileRecords, roundViewModel.Blue1TeamNumber, round.MatchNumber);
                bool hasBlue2Data = HaveMatchData(teamFileRecords, roundViewModel.Blue2TeamNumber, round.MatchNumber);
                bool hasBlue3Data = HaveMatchData(teamFileRecords, roundViewModel.Blue3TeamNumber, round.MatchNumber);

                roundViewModel.Red1BackColor = hasRed1Data ? darkRedBrush : red1Brush;
                roundViewModel.Red2BackColor = hasRed2Data ? darkRedBrush : red2Brush;
                roundViewModel.Red3BackColor = hasRed3Data ? darkRedBrush : red3Brush;
                roundViewModel.Blue1BackColor = hasBlue1Data ? darkBlueBrush : blue1Brush;
                roundViewModel.Blue2BackColor = hasBlue2Data ? darkBlueBrush : blue2Brush;
                roundViewModel.Blue3BackColor = hasBlue3Data ? darkBlueBrush : blue3Brush;

                Schedule.Add(roundViewModel);
            }

            this.Station = "Scouting Station: " + config.Station.ToUpper();
        }

        private bool HaveMatchData(List<TeamFileRecord2022> teamFileRecords, int teamNumber, int matchNumber)
        {
            TeamFileRecord2022 teamFileRecord = teamFileRecords.FirstOrDefault(record => record.TeamNumber == teamNumber);
            if (teamFileRecord == null) return false;
            TeamFileRecord2022.Round matchData = teamFileRecord.Rounds.FirstOrDefault(round => round.Number == matchNumber);
            if (matchData == null) return false;

            return matchData.HasData;
        }
    }
}
