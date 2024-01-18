using VScoutCommon.Common;
using VScoutCommon.ViewModels;

namespace ScoutingDataCentral.ViewModels
{
    public class JustInTimeTeamRectangleViewModel2022 : BaseViewModel
    {
        public JustInTimeTeamRectangleViewModel2022(int teamNumber)
        {
            this.ImagePath = TeamFile.GetTeamImagePath(Constants.CentralBaseFilePath, teamNumber);
            this.TeamNumber = teamNumber;
        }

        private string station;
        public string Station
        {
            get { return this.station; }
            set
            {
                this.station = value;
                OnPropertyChanged(nameof(Station));
            }
        }

        private int teamNumber;
        public int TeamNumber
        {
            get { return this.teamNumber; }
            set
            {
                this.teamNumber = value;
                OnPropertyChanged(nameof(TeamNumber));
            }
        }

        private string imagePath;
        public string ImagePath
        {
            get { return this.imagePath; }
            set
            {
                this.imagePath = value;
                OnPropertyChanged(nameof(ImagePath));
            }
        }

        private int roundsPlayed;
        public int RoundsPlayed
        {
            get
            {
                return roundsPlayed;
            }
            set
            {
                this.roundsPlayed = value;
                OnPropertyChanged(nameof(RoundsPlayed));
            }
        }

        private int rank;
        public int Rank
        {
            get { return this.rank; }
            set
            {
                this.rank = value;
                OnPropertyChanged(nameof(Rank));
            }
        }

        private decimal opr;
        public decimal Opr
        {
            get { return this.opr; }
            set
            {
                this.opr = value;
                OnPropertyChanged(nameof(Opr));
            }
        }

        private decimal averageHubPoints;
        public decimal AverageHubPoints
        {
            get { return this.averageHubPoints; }
            set
            {
                this.averageHubPoints = value;
                OnPropertyChanged(nameof(AverageHubPoints));
            }
        }

        private int lowBar;
        public int LowBar
        {
            get { return this.lowBar; }
            set
            {
                this.lowBar = value;
                OnPropertyChanged(nameof(LowBar));
            }
        }

        private int secondBar;
        public int SecondBar
        {
            get { return this.secondBar; }
            set
            {
                this.secondBar = value;
                OnPropertyChanged(nameof(SecondBar));
            }
        }

        private int thirdBar;
        public int ThirdBar
        {
            get { return this.thirdBar; }
            set
            {
                this.thirdBar = value;
                OnPropertyChanged(nameof(ThirdBar));
            }
        }

        private int highBar;
        public int HighBar
        {
            get { return this.highBar; }
            set
            {
                this.highBar = value;
                OnPropertyChanged(nameof(HighBar));
            }
        }

        private string commments;
        public string Comments
        {
            get
            {
                return this.commments;
            }
            set
            {
                this.commments = value;
                OnPropertyChanged(nameof(Comments));
            }
        }
    }
}
