using System.IO;
using System.Linq;
using VScoutCommon.Common;
using VScoutCommon.Models.BusinessModels;

namespace VScoutDataCollector.ViewModels
{
    public class ScoutInputViewModel2020 : ScoutInputViewModelBase
    {
        #region Autonomous
        private bool moved;
        public bool Moved
        {
            get { return this.moved; }
            set
            {
                this.moved = value;
                OnPropertyChanged(nameof(Moved));
            }
        }

        private int lowGoalAutonomous;
        public int LowGoalAutonomous
        {
            get { return this.lowGoalAutonomous; }
            set
            {
                this.lowGoalAutonomous = value;
                OnPropertyChanged(nameof(LowGoalAutonomous));
            }
        }

        private int highGoalAutonomous;
        public int HighGoalAutonomous
        {
            get { return this.highGoalAutonomous; }
            set
            {
                this.highGoalAutonomous = value;
                OnPropertyChanged(nameof(HighGoalAutonomous));
            }
        }
        #endregion

        #region PowerPort
        private int lowGoal;
        public int LowGoal
        {
            get { return this.lowGoal; }
            set
            {
                this.lowGoal = value;
                OnPropertyChanged(nameof(LowGoal));
            }
        }

        private int highGoal;
        public int HighGoal
        {
            get { return this.highGoal; }
            set
            {
                this.highGoal = value;
                OnPropertyChanged(nameof(HighGoal));
            }
        }
        #endregion

        #region ControlPanel
        private bool achievedRotationControl;
        public bool AchievedRotationControl
        {
            get { return this.achievedRotationControl; }
            set
            {
                this.achievedRotationControl = value;
                OnPropertyChanged(nameof(AchievedRotationControl));
            }
        }

        private bool achievedPositionControl;
        public bool AchievedPositionControl
        {
            get { return this.achievedPositionControl; }
            set
            {
                this.achievedPositionControl = value;
                OnPropertyChanged(nameof(AchievedPositionControl));
            }
        }
        #endregion

        #region Endgame

        private bool achievedClimb;
        public bool AchievedClimb
        {
            get { return this.achievedClimb; }
            set
            {
                this.achievedClimb = value;
                OnPropertyChanged(nameof(AchievedClimb));
            }
        }

        private bool achievedBalance;
        public bool AchievedBalance
        {
            get { return this.achievedBalance; }
            set
            {
                this.achievedBalance = value;
                OnPropertyChanged(nameof(AchievedBalance));
            }
        }

        private bool getBackDown;
        public bool GetBackDown
        {
            get { return this.getBackDown; }
            set
            {
                this.getBackDown = value;
                OnPropertyChanged(nameof(GetBackDown));
            }
        }

        private bool goUnderTurntable;
        public bool GoUnderTurntable
        {
            get
            {
                return this.goUnderTurntable;
            }
            set
            {
                this.goUnderTurntable = value;
                OnPropertyChanged(nameof(GoUnderTurntable));
            }
        }

        private bool goThroughCity;
        public bool GoThroughCity
        {
            get
            {
                return this.goThroughCity;
            }
            set
            {
                this.goThroughCity = value;
                OnPropertyChanged(nameof(GoThroughCity));
            }
        }
        #endregion

        public void Open()
        {
            this.ImagePath = Path.Combine(Constants.CollectorBaseFilePath, TeamNumber.ToString(), $"{TeamNumber}.jpg");
            string filepath = GetFilePath();

            if (!File.Exists(filepath)) return;

            TeamFileRecord2020 teamFileRecord = TeamFile.Parse(File.ReadAllText(filepath));
            TeamFileRecord2020.Round round = teamFileRecord.Rounds.FirstOrDefault(r => r.Number == RoundNumber);

            this.Name = teamFileRecord.Name;
            this.SchoolName = teamFileRecord.SchoolName;
            this.City = teamFileRecord.City;
            this.State = teamFileRecord.State;
            this.RookieYear = teamFileRecord.RookieYear;

            if (round == null) return;

            this.GoThroughCity = round.GoThroughCity;
            this.GoUnderTurntable = round.GoUnderTurntable;

            // Autonomous
            this.Moved = round.Autonomous.Moved;
            this.LowGoalAutonomous = round.Autonomous.PowerPort.LowGoal;
            this.HighGoalAutonomous = round.Autonomous.PowerPort.HighGoal;

            // Power Port
            this.LowGoal = round.PowerPort.LowGoal;
            this.HighGoal = round.PowerPort.HighGoal;

            // Control Panel
            this.AchievedRotationControl = round.ControlPanel.AchievedRotationControl;
            this.AchievedPositionControl = round.ControlPanel.AchievedPositionControl;

            // Endgame
            this.AchievedClimb = round.Endgame.AchievedClimb;
            this.AchievedBalance = round.Endgame.AchievedBalance;
            this.GetBackDown = round.Endgame.GetBackDown;

            this.Comments = round.Comments;
        }

        public void Save()
        {
            TeamFileRecord2020 teamFileRecord = new TeamFileRecord2020();
            teamFileRecord.TeamNumber = this.TeamNumber;

            TeamFileRecord2020.Round round = new TeamFileRecord2020.Round();
            round.Number = this.RoundNumber;
            round.Comments = this.Comments;

            round.GoUnderTurntable = this.GoUnderTurntable;
            round.GoThroughCity = this.GoThroughCity;

            // Autonomous
            round.Autonomous.Moved = this.Moved;
            round.Autonomous.PowerPort.LowGoal = this.LowGoalAutonomous;
            round.Autonomous.PowerPort.HighGoal = this.HighGoalAutonomous;

            // Power Port
            round.PowerPort.LowGoal = this.LowGoal;
            round.PowerPort.HighGoal = this.HighGoal;

            // Control Panel
            round.ControlPanel.AchievedRotationControl = this.AchievedRotationControl;
            round.ControlPanel.AchievedPositionControl = this.AchievedPositionControl;

            // Endgame
            round.Endgame.AchievedBalance = this.AchievedBalance;
            round.Endgame.AchievedClimb = this.AchievedClimb;
            round.Endgame.GetBackDown = this.GetBackDown;

            teamFileRecord.Rounds.Add(round);

            TeamFile.Save(Constants.CollectorBaseFilePath, teamFileRecord);
        }
    }
}
