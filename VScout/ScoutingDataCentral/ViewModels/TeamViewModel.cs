using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VScoutCommon.ViewModels;

namespace ScoutingDataCentral.ViewModels
{
    public class TeamViewModel : BaseViewModel
    {
        private int number;
        public int Number
        {
            get
            {
                return this.number;
            }
            set
            {
                this.number = value;
                OnPropertyChanged(nameof(Number));
            }
        }

        private string name;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private int rank;
        public int Rank
        {
            get
            {
                return this.rank;
            }
            set
            {
                this.rank = value;
                OnPropertyChanged(nameof(Rank));
            }
        }

        private decimal opr;
        public decimal Opr
        {
            get
            {
                return this.opr;
            }
            set
            {
                this.opr = value;
                OnPropertyChanged(nameof(Opr));
            }
        }

        private decimal averagePowerCellPoints;
        public decimal AveragePowerCellPoints
        {
            get
            {
                return this.averagePowerCellPoints;
            }
            set
            {
                this.averagePowerCellPoints = value;
                OnPropertyChanged(nameof(AveragePowerCellPoints));
            }
        }

        private decimal rotationalControlCount;
        public decimal RotationalControlCount
        {
            get
            {
                return this.rotationalControlCount;
            }
            set
            {
                this.rotationalControlCount = value;
                OnPropertyChanged(nameof(RotationalControlCount));
            }
        }

        private decimal positionalControlCount;
        public decimal PositionalControlCount
        {
            get
            {
                return this.positionalControlCount;
            }
            set
            {
                this.positionalControlCount = value;
                OnPropertyChanged(nameof(PositionalControlCount));
            }
        }

        private decimal hangCount;
        public decimal HangCount
        {
            get
            {
                return this.hangCount;
            }
            set
            {
                this.hangCount = value;
                OnPropertyChanged(nameof(HangCount));
            }
        }

        private decimal parkCount;
        public decimal ParkCount
        {
            get
            {
                return this.parkCount;
            }
            set
            {
                this.parkCount = value;
                OnPropertyChanged(nameof(ParkCount));
            }
        }
    }
}
