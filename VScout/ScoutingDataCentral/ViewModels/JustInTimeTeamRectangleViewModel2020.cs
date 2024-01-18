using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VScoutCommon.Common;
using VScoutCommon.ViewModels;

namespace ScoutingDataCentral.ViewModels
{
    public class JustInTimeTeamRectangleViewModel2020 : BaseViewModel
    {
        public JustInTimeTeamRectangleViewModel2020(int teamNumber)
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

        private decimal averagePowerCellPoints;
        public decimal AveragePowerCellPoints
        {
            get { return this.averagePowerCellPoints; }
            set
            {
                this.averagePowerCellPoints = value;
                OnPropertyChanged(nameof(AveragePowerCellPoints));
            }
        }

        private int rotationControlCount;
        public int RotationControlCount
        {
            get { return this.rotationControlCount; }
            set
            {
                this.rotationControlCount = value;
                OnPropertyChanged(nameof(RotationControlCount));
            }
        }

        private int positionControlCount;
        public int PositionControlCount
        {
            get { return this.positionControlCount; }
            set
            {
                this.positionControlCount = value;
                OnPropertyChanged(nameof(PositionControlCount));
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

        private int hangCount;
        public int ClimbCount
        {
            get { return this.hangCount; }
            set
            {
                this.hangCount = value;
                OnPropertyChanged(nameof(ClimbCount));
            }
        }

        private int parkCount;
        public int BalanceCount
        {
            get { return this.parkCount; }
            set
            {
                this.parkCount = value;
                OnPropertyChanged(nameof(BalanceCount));
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
