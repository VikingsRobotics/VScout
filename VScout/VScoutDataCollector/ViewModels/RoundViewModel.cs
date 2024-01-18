using System.Windows.Media;
using VScoutCommon.ViewModels;

namespace VScoutDataCollector.ViewModels
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

        private Brush red1BackColor;
        public Brush Red1BackColor
        {
            get
            {
                return this.red1BackColor;
            }
            set
            {
                this.red1BackColor = value;
                OnPropertyChanged(nameof(Red1BackColor));
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

        private Brush red2BackColor;
        public Brush Red2BackColor
        {
            get
            {
                return this.red2BackColor;
            }
            set
            {
                this.red2BackColor = value;
                OnPropertyChanged(nameof(Red2BackColor));
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

        private Brush red3BackColor;
        public Brush Red3BackColor
        {
            get
            {
                return this.red3BackColor;
            }
            set
            {
                this.red3BackColor = value;
                OnPropertyChanged(nameof(Red3BackColor));
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

        private Brush blue1BackColor;
        public Brush Blue1BackColor
        {
            get
            {
                return this.blue1BackColor;
            }
            set
            {
                this.blue1BackColor = value;
                OnPropertyChanged(nameof(Blue1BackColor));
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

        private Brush blue2BackColor;
        public Brush Blue2BackColor
        {
            get
            {
                return this.blue2BackColor;
            }
            set
            {
                this.blue2BackColor = value;
                OnPropertyChanged(nameof(Blue2BackColor));
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

        private Brush blue3BackColor;
        public Brush Blue3BackColor
        {
            get
            {
                return this.blue3BackColor;
            }
            set
            {
                this.blue3BackColor = value;
                OnPropertyChanged(nameof(Blue3BackColor));
            }
        }
    }
}
