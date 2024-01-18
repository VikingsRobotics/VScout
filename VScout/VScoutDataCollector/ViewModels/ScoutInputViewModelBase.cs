using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VScoutCommon.Common;
using VScoutCommon.ViewModels;

namespace VScoutDataCollector.ViewModels
{
    public class ScoutInputViewModelBase : BaseViewModel
    {
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

        private int roundNumber;
        public int RoundNumber
        {
            get { return this.roundNumber; }
            set
            {
                this.roundNumber = value;
                OnPropertyChanged(nameof(RoundNumber));
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

        private string name;
        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string schoolName;
        public string SchoolName
        {
            get { return this.schoolName; }
            set
            {
                this.schoolName = value;
                OnPropertyChanged(nameof(SchoolName));
            }
        }

        private string city;
        public string City
        {
            get { return this.city; }
            set
            {
                this.city = value;
                OnPropertyChanged(nameof(City));
            }
        }

        private string state;
        public string State
        {
            get { return this.state; }
            set
            {
                this.state = value;
                OnPropertyChanged(nameof(State));
            }
        }

        private int rookieYear;
        public int RookieYear
        {
            get { return this.rookieYear; }
            set
            {
                this.rookieYear = value;
                OnPropertyChanged(nameof(RookieYear));
            }
        }

        private string comments = string.Empty;
        public string Comments
        {
            get { return this.comments; }
            set
            {
                this.comments = value;
                OnPropertyChanged(nameof(Comments));
            }
        }

        protected string GetFilePath()
        {
            return Path.Combine(Constants.CollectorBaseFilePath, TeamNumber.ToString(), $"{TeamNumber}.xml");
        }
    }
}
