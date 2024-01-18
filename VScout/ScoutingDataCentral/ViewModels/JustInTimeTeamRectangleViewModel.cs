using VScoutCommon.Common;
using VScoutCommon.ViewModels;

namespace ScoutingDataCentral.ViewModels
{
    public class JustInTimeTeamRectangleViewModel : BaseViewModel
    {
        public JustInTimeTeamRectangleViewModel(int teamNumber)
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

        private decimal averageHatches;
        public decimal AverageHatches
        {
            get { return this.averageHatches; }
            set
            {
                this.averageHatches = value;
                OnPropertyChanged(nameof(AverageHatches));
            }
        }

        private decimal averageCargo;
        public decimal AverageCargo
        {
            get { return this.averageCargo; }
            set
            {
                this.averageCargo = value;
                OnPropertyChanged(nameof(AverageCargo));
            }
        }

        private string climb;
        public string Climb
        {
            get { return this.climb; }
            set
            {
                this.climb = value;
                OnPropertyChanged(nameof(Climb));
            }
        }

        private string comments;
        public string Comments
        {
            get { return this.comments; }
            set
            {
                this.comments = value;
                OnPropertyChanged(nameof(Comments));
            }
        }
    }
}
