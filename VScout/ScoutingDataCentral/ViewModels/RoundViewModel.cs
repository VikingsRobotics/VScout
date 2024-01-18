using VScoutCommon.ViewModels;

namespace ScoutingDataCentral.ViewModels
{
    public class RoundViewModel : BaseViewModel
    {
        private int matchNumber;
        public int MatchNumber
        {
            get
            {
                return this.matchNumber;
            }

            set
            {
                this.matchNumber = value;
                OnPropertyChanged(nameof(MatchNumber));
            }
        }

        private int red1TeamNumber;
        public int Red1TeamNumber
        {
            get
            {
                return this.red1TeamNumber;
            }

            set
            {
                this.red1TeamNumber = value;
                OnPropertyChanged(nameof(Red1TeamNumber));
            }
        }

        private int red2TeamNumber;
        public int Red2TeamNumber
        {
            get
            {
                return this.red2TeamNumber;
            }

            set
            {
                this.red2TeamNumber = value;
                OnPropertyChanged(nameof(Red2TeamNumber));
            }
        }

        private int red3TeamNumber;
        public int Red3TeamNumber
        {
            get
            {
                return this.red3TeamNumber;
            }

            set
            {
                this.red3TeamNumber = value;
                OnPropertyChanged(nameof(Red3TeamNumber));
            }
        }

        private int blue1TeamNumber;
        public int Blue1TeamNumber
        {
            get
            {
                return this.blue1TeamNumber;
            }

            set
            {
                this.blue1TeamNumber = value;
                OnPropertyChanged(nameof(Blue1TeamNumber));
            }
        }

        private int blue2TeamNumber;
        public int Blue2TeamNumber
        {
            get
            {
                return this.blue2TeamNumber;
            }

            set
            {
                this.blue2TeamNumber = value;
                OnPropertyChanged(nameof(Blue2TeamNumber));
            }
        }

        private int blue3TeamNumber;
        public int Blue3TeamNumber
        {
            get
            {
                return this.blue3TeamNumber;
            }

            set
            {
                this.blue3TeamNumber = value;
                OnPropertyChanged(nameof(Blue3TeamNumber));
            }
        }
    }
}
