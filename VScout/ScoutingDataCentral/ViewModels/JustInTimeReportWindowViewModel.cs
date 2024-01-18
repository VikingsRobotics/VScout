using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using VScoutCommon.Common;
using VScoutCommon.Models;
using VScoutCommon.Models.BusinessModels;
using VScoutCommon.ViewModels;

namespace ScoutingDataCentral.ViewModels
{
    public class JustInTimeReportWindowViewModel : BaseViewModel
    {
        private string message;
        public string Message
        {
            get { return this.message; }
            set
            {
                this.message = value;
                OnPropertyChanged(nameof(Message));
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

        private ObservableCollection<JustInTimeTeamRectangleViewModel2022> jitRedTeamTiles = new ObservableCollection<JustInTimeTeamRectangleViewModel2022>();
        public ObservableCollection<JustInTimeTeamRectangleViewModel2022> JitRedTeamTiles
        {
            get { return this.jitRedTeamTiles; }
            set
            {
                this.jitRedTeamTiles = value;
                OnPropertyChanged(nameof(JitRedTeamTiles));
            }
        }

        private ObservableCollection<JustInTimeTeamRectangleViewModel2022> jitBlueTeamTiles = new ObservableCollection<JustInTimeTeamRectangleViewModel2022>();
        public ObservableCollection<JustInTimeTeamRectangleViewModel2022> JitBlueTeamTiles
        {
            get { return this.jitBlueTeamTiles; }
            set
            {
                this.jitBlueTeamTiles = value;
                OnPropertyChanged(nameof(JitBlueTeamTiles));
            }
        }

        private int scheduleWindowRound;
        public int ScheduleWindowRound
        {
            get
            {
                return this.scheduleWindowRound;
            }
            set
            {
                this.scheduleWindowRound = value;
                OnPropertyChanged(nameof(ScheduleWindowRound));
            }
        }

        public void LoadSchedule(string scheduleFileName)
        {
            string fileContents = File.ReadAllText(scheduleFileName);

            Schedule schedule = JsonConvert.DeserializeObject<Schedule>(fileContents);

            foreach (Round round in schedule.Rounds)
            {
                this.Schedule.Add(new RoundViewModel
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

        private JustInTimeTeamRectangleViewModel2022 CreateRectangleViewModel(TeamFileRecord2022 teamFileRecord, string station)
        {
            return new JustInTimeTeamRectangleViewModel2022(teamFileRecord.TeamNumber)
            {
                Rank = teamFileRecord.Rank,
                Opr = teamFileRecord.Opr,
                RoundsPlayed = teamFileRecord.Rounds.Count,
                Station = station,
                AverageHubPoints = teamFileRecord.AverageHubPoints,
                LowBar = teamFileRecord.LowBarCount,
                SecondBar = teamFileRecord.SecondBarCount,
                ThirdBar = teamFileRecord.ThirdBarCount,
                HighBar = teamFileRecord.HighBarCount,
                Comments = teamFileRecord.Comments
            };
        }

        public void DetermineRoundWhenAllPreviousDataIsComplete(int roundNumberToSearchBefore)
        {
            List<int> teamsToSearchFor = new List<int>();

            RoundViewModel roundToSearchBefore = this.Schedule.FirstOrDefault(r => r.MatchNumber == roundNumberToSearchBefore);
            teamsToSearchFor.Add(roundToSearchBefore.Red1TeamNumber);
            teamsToSearchFor.Add(roundToSearchBefore.Red2TeamNumber);
            teamsToSearchFor.Add(roundToSearchBefore.Red3TeamNumber);
            teamsToSearchFor.Add(roundToSearchBefore.Blue1TeamNumber);
            teamsToSearchFor.Add(roundToSearchBefore.Blue2TeamNumber);
            teamsToSearchFor.Add(roundToSearchBefore.Blue3TeamNumber);

            foreach (RoundViewModel round in this.Schedule.Where(r => r.MatchNumber < roundNumberToSearchBefore).OrderByDescending(r => r.MatchNumber))
            {
                if (teamsToSearchFor.Contains(round.Red1TeamNumber) || teamsToSearchFor.Contains(round.Red2TeamNumber) || teamsToSearchFor.Contains(round.Red3TeamNumber) ||
                    teamsToSearchFor.Contains(round.Blue1TeamNumber) || teamsToSearchFor.Contains(round.Blue2TeamNumber) || teamsToSearchFor.Contains(round.Blue3TeamNumber))
                {
                    this.ScheduleWindowRound = round.MatchNumber;
                    return;
                }
            }

            return;
        }

        public void LoadJitTeamTiles(RoundViewModel roundViewModel)
        {
            this.JitBlueTeamTiles.Clear();
            this.JitRedTeamTiles.Clear();

            TeamFileRecord2022 oRed1TeamFile = TeamFile2022.Get(Constants.CentralBaseFilePath, roundViewModel.Red1TeamNumber);
            TeamFileRecord2022 oRed2TeamFile = TeamFile2022.Get(Constants.CentralBaseFilePath, roundViewModel.Red2TeamNumber);
            TeamFileRecord2022 oRed3TeamFile = TeamFile2022.Get(Constants.CentralBaseFilePath, roundViewModel.Red3TeamNumber);

            TeamFileRecord2022 oBlue1TeamFile = TeamFile2022.Get(Constants.CentralBaseFilePath, roundViewModel.Blue1TeamNumber);
            TeamFileRecord2022 oBlue2TeamFile = TeamFile2022.Get(Constants.CentralBaseFilePath, roundViewModel.Blue2TeamNumber);
            TeamFileRecord2022 oBlue3TeamFile = TeamFile2022.Get(Constants.CentralBaseFilePath, roundViewModel.Blue3TeamNumber);

            if (oRed1TeamFile != null)
            {
                this.JitRedTeamTiles.Add(CreateRectangleViewModel(oRed1TeamFile, "Red 1"));
            }
            else
            {
                this.JitRedTeamTiles.Add(new JustInTimeTeamRectangleViewModel2022(roundViewModel.Red1TeamNumber)
                {
                    Station = "Red 1"
                });
            }

            if (oRed2TeamFile != null)
            {
                this.JitRedTeamTiles.Add(CreateRectangleViewModel(oRed2TeamFile, "Red 2"));
            }
            else
            {
                this.JitRedTeamTiles.Add(new JustInTimeTeamRectangleViewModel2022(roundViewModel.Red2TeamNumber)
                {
                    Station = "Red 2"
                });
            }

            if (oRed3TeamFile != null)
            {
                this.JitRedTeamTiles.Add(CreateRectangleViewModel(oRed3TeamFile, "Red 3"));
            }
            else
            {
                this.JitRedTeamTiles.Add(new JustInTimeTeamRectangleViewModel2022(roundViewModel.Red3TeamNumber)
                {
                    Station = "Red 3"
                });
            }

            if (oBlue1TeamFile != null)
            {
                this.JitBlueTeamTiles.Add(CreateRectangleViewModel(oBlue1TeamFile, "Blue 1"));
            }
            else
            {
                this.JitBlueTeamTiles.Add(new JustInTimeTeamRectangleViewModel2022(roundViewModel.Blue1TeamNumber)
                {
                    Station = "Blue 1"
                });
            }

            if (oBlue2TeamFile != null)
            {
                this.JitBlueTeamTiles.Add(CreateRectangleViewModel(oBlue2TeamFile, "Blue 2"));
            }
            else
            {
                this.JitBlueTeamTiles.Add(new JustInTimeTeamRectangleViewModel2022(roundViewModel.Blue2TeamNumber)
                {
                    Station = "Blue 2"
                });
            }

            if (oBlue3TeamFile != null)
            {
                this.JitBlueTeamTiles.Add(CreateRectangleViewModel(oBlue3TeamFile, "Blue 3"));
            }
            else
            {
                this.JitBlueTeamTiles.Add(new JustInTimeTeamRectangleViewModel2022(roundViewModel.Blue3TeamNumber)
                {
                    Station = "Blue 3"
                });
            }
        }
    }
}
