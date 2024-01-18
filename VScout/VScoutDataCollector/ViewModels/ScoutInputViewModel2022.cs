using System.IO;
using System.Linq;
using VScoutCommon.Common;
using VScoutCommon.Models.BusinessModels;

namespace VScoutDataCollector.ViewModels
{
    public class ScoutInputViewModel2022 : ScoutInputViewModelBase
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

        #region Hub
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

        #region Endgame

        private bool lowBar;
        public bool LowBar
        {
            get { return this.lowBar; }
            set
            {
                this.lowBar = value;
                OnPropertyChanged(nameof(LowBar));
            }
        }

        private bool secondBar;
        public bool SecondBar
        {
            get { return this.secondBar; }
            set
            {
                this.secondBar = value;
                OnPropertyChanged(nameof(SecondBar));
            }
        }

        private bool thirdBar;
        public bool ThirdBar
        {
            get { return this.thirdBar; }
            set
            {
                this.thirdBar = value;
                OnPropertyChanged(nameof(ThirdBar));
            }
        }

        private bool highBar;
        public bool HighBar
        {
            get { return this.highBar; }
            set
            {
                this.highBar = value;
                OnPropertyChanged(nameof(HighBar));
            }
        }
        #endregion

        public void Open()
        {
            this.ImagePath = Path.Combine(Constants.CollectorBaseFilePath, TeamNumber.ToString(), $"{TeamNumber}.jpg");
            string filepath = GetFilePath();

            if (!File.Exists(filepath)) return;

            TeamFileRecord2022 teamFileRecord = TeamFile2022.Parse(File.ReadAllText(filepath));
            TeamFileRecord2022.Round round = teamFileRecord.Rounds.FirstOrDefault(r => r.Number == RoundNumber);

            this.Name = teamFileRecord.Name;
            this.SchoolName = teamFileRecord.SchoolName;
            this.City = teamFileRecord.City;
            this.State = teamFileRecord.State;
            this.RookieYear = teamFileRecord.RookieYear;

            if (round == null) return;

            // Autonomous
            this.Moved = round.Autonomous.Moved;
            this.LowGoalAutonomous = round.Autonomous.Hub.LowGoal;
            this.HighGoalAutonomous = round.Autonomous.Hub.HighGoal;

            // Power Port
            this.LowGoal = round.Hub.LowGoal;
            this.HighGoal = round.Hub.HighGoal;

            // Endgame
            this.LowBar = round.Endgame.LowRung;
            this.SecondBar = round.Endgame.SecondRung;
            this.ThirdBar = round.Endgame.ThirdRung;
            this.HighBar = round.Endgame.HighRung;

            this.Comments = round.Comments;
        }

        public void Save()
        {
            TeamFileRecord2022 teamFileRecord = new TeamFileRecord2022();
            teamFileRecord.TeamNumber = this.TeamNumber;

            TeamFileRecord2022.Round round = new TeamFileRecord2022.Round();
            round.Number = this.RoundNumber;
            round.Comments = this.Comments;

            // Autonomous
            round.Autonomous.Moved = this.Moved;
            round.Autonomous.Hub.LowGoal = this.LowGoalAutonomous;
            round.Autonomous.Hub.HighGoal = this.HighGoalAutonomous;

            // Power Port
            round.Hub.LowGoal = this.LowGoal;
            round.Hub.HighGoal = this.HighGoal;

            // Endgame
            round.Endgame.LowRung = this.LowBar;
            round.Endgame.SecondRung = this.SecondBar;
            round.Endgame.ThirdRung = this.ThirdBar;
            round.Endgame.HighRung = this.HighBar;

            teamFileRecord.Rounds.Add(round);

            TeamFile2022.Save(Constants.CollectorBaseFilePath, teamFileRecord);
        }
    }
}
