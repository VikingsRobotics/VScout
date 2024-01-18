using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using VScoutCommon.Models;
using VScoutCommon.ViewModels;

namespace ScoutingDataCentral.ViewModels
{
    public class ImportScoutReportViewModel : BaseViewModel
    {
        private const string RootPath = @"C:\VScout\Central\2019\";

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

        public void LoadSchedule(string scheduleFileName)
        {
            string fileContents = File.ReadAllText(scheduleFileName);

            Schedule schedule = JsonConvert.DeserializeObject<Schedule>(fileContents);

            foreach (Round round in schedule.Rounds)
            {
                Schedule.Add(new RoundViewModel
                {
                    MatchNumber = round.MatchNumber,
                    Red1TeamNumber = round.Teams.First(team => team.Station == "Red1").TeamNumber,
                    Red2TeamNumber = round.Teams.First(team => team.Station == "Red2").TeamNumber,
                    Red3TeamNumber = round.Teams.First(team => team.Station == "Red3").TeamNumber,
                    Blue1TeamNumber = round.Teams.First(team => team.Station == "Blue1").TeamNumber,
                    Blue2TeamNumber = round.Teams.First(team => team.Station == "Blue2").TeamNumber,
                    Blue3TeamNumber = round.Teams.First(team => team.Station == "Blue3").TeamNumber
                });
            }
        }

        /// <summary>
        /// Syncs the files.
        /// </summary>
        public void SyncFiles()
        {
            FolderBrowserDialog openFolderDialog = new FolderBrowserDialog();

            DialogResult result = openFolderDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string[] filesToImport = Directory.GetFiles(openFolderDialog.SelectedPath, "*.xml");

                foreach (string fileToImport in filesToImport)
                {
                    string pathToCentralTeamFile = Path.Combine(RootPath, Path.GetFileNameWithoutExtension(fileToImport), Path.GetFileName(fileToImport));

                    if (!File.Exists(pathToCentralTeamFile))
                    {
                        File.Copy(fileToImport, pathToCentralTeamFile);
                        continue;
                    }

                    XElement rootCentral = XElement.Parse(File.ReadAllText(pathToCentralTeamFile));
                    XElement rootImport = XElement.Parse(File.ReadAllText(fileToImport));

                    foreach (XElement round in rootImport.Elements("Round"))
                    {
                        int importRoundNumber = (int)round.Attribute("number");
                        XElement centralRound = rootCentral.Elements("Round").FirstOrDefault(e => (int)e.Attribute("number") == importRoundNumber);

                        if (centralRound == null)
                        {
                            rootCentral.Add(round);
                        }
                    }

                    rootCentral.Save(pathToCentralTeamFile);
                }
            }
        }
    }
}
