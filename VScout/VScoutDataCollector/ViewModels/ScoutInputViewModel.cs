using System.IO;
using System.Linq;
using System.Xml.Linq;
using VScoutCommon.Common;
using VScoutCommon.Models.BusinessModels;
using VScoutCommon.ViewModels;

namespace VScoutDataCollector.ViewModels
{
    public class ScoutInputViewModel : ScoutInputViewModelBase
    {
        private const string RootPath = @"C:\VScout\Collector\2019\";

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

        private bool usedCamera;
        public bool UsedCamera
        {
            get { return this.usedCamera; }
            set
            {
                this.usedCamera = value;
                OnPropertyChanged(nameof(UsedCamera));
            }
        }

        private bool decendedPlatform;
        public bool DescendedPlatform
        {
            get { return this.decendedPlatform; }
            set
            {
                this.decendedPlatform = value;
                OnPropertyChanged(nameof(DescendedPlatform));
            }
        }
        #endregion

        #region Hatches
        private int cargoShipHatchCount;
        public int CargoShipHatchCount
        {
            get { return this.cargoShipHatchCount; }
            set
            {
                this.cargoShipHatchCount = value;
                OnPropertyChanged(nameof(this.CargoShipHatchCount));
            }
        }

        private int levelOneHatchCount;
        public int LevelOneHatchCount
        {
            get { return this.levelOneHatchCount; }
            set
            {
                this.levelOneHatchCount = value;
                OnPropertyChanged(nameof(LevelOneHatchCount));
            }
        }

        private int levelTwoHatchCount;
        public int LevelTwoHatchCount
        {
            get { return this.levelTwoHatchCount; }
            set
            {
                this.levelTwoHatchCount = value;
                OnPropertyChanged(nameof(LevelTwoHatchCount));
            }
        }

        private int levelThreeHatchCount;
        public int LevelThreeHatchCount
        {
            get { return this.levelThreeHatchCount; }
            set
            {
                this.levelThreeHatchCount = value;
                OnPropertyChanged(nameof(LevelThreeHatchCount));
            }
        }
        #endregion

        #region Cargo
        private int cargoShipCargoCount;
        public int CargoShipCargoCount
        {
            get { return this.cargoShipCargoCount; }
            set
            {
                this.cargoShipCargoCount = value;
                OnPropertyChanged(nameof(this.CargoShipCargoCount));
            }
        }

        private int levelOneCargoCount;
        public int LevelOneCargoCount
        {
            get { return this.levelOneCargoCount; }
            set
            {
                this.levelOneCargoCount = value;
                OnPropertyChanged(nameof(LevelOneCargoCount));
            }
        }

        private int levelTwoCargoCount;
        public int LevelTwoCargoCount
        {
            get { return this.levelTwoCargoCount; }
            set
            {
                this.levelTwoCargoCount = value;
                OnPropertyChanged(nameof(LevelTwoCargoCount));
            }
        }

        private int levelThreeCargoCount;
        public int LevelThreeCargoCount
        {
            get { return this.levelThreeCargoCount; }
            set
            {
                this.levelThreeCargoCount = value;
                OnPropertyChanged(nameof(LevelThreeCargoCount));
            }
        }
        #endregion

        public void Open()
        {
            this.ImagePath = Path.Combine(RootPath, TeamNumber.ToString(), $"{TeamNumber}.jpg");
            string filepath = GetFilePath();

            if (!File.Exists(filepath)) return;

            TeamFileRecord teamFileRecord = new TeamFileRecord(); //TeamFile.Parse(File.ReadAllText(filepath));
            TeamFileRecord.Round round = teamFileRecord.Rounds.FirstOrDefault(r => r.Number == RoundNumber);

            if (round == null) return;

            this.Moved = round.Autonomous.Moved;
            this.UsedCamera = round.Autonomous.Camera;
            this.DescendedPlatform = round.Autonomous.DescendedPlatform;

            this.CargoShipHatchCount = round.Hatches.CargoShip;
            this.LevelOneHatchCount = round.Hatches.Level1;
            this.LevelTwoHatchCount = round.Hatches.Level2;
            this.LevelThreeHatchCount = round.Hatches.Level3;

            this.CargoShipCargoCount = round.Cargo.CargoShip;
            this.LevelOneCargoCount = round.Cargo.Level1;
            this.LevelTwoCargoCount = round.Cargo.Level2;
            this.LevelThreeCargoCount = round.Cargo.Level3;

            this.Comments = round.Comments;
        }

        public void Save()
        {
            TeamFileRecord teamFileRecord = new TeamFileRecord();
            teamFileRecord.TeamNumber = this.TeamNumber;

            TeamFileRecord.Round round = new TeamFileRecord.Round();
            round.Number = this.RoundNumber;
            round.Comments = this.Comments;
            round.Autonomous.Moved = this.Moved;
            round.Autonomous.DescendedPlatform = this.DescendedPlatform;
            round.Autonomous.Camera = this.UsedCamera;

            round.Hatches.CargoShip = this.CargoShipHatchCount;
            round.Hatches.Level1 = this.LevelOneHatchCount;
            round.Hatches.Level2 = this.LevelTwoHatchCount;
            round.Hatches.Level3 = this.LevelThreeHatchCount;

            round.Cargo.CargoShip = this.CargoShipCargoCount;
            round.Cargo.Level1 = this.LevelOneCargoCount;
            round.Cargo.Level2 = this.LevelTwoCargoCount;
            round.Cargo.Level3 = this.LevelThreeCargoCount;

            teamFileRecord.Rounds.Add(round);

           // TeamFile.Save(Constants.CollectorBaseFilePath, teamFileRecord);
        }

        private string ConvertBoolToString(bool value)
        {
            return value ? "Y" : "N";
        }

        private bool ConvertStringToBool(string value)
        {
            return value.ToUpper() == "Y";
        }
    }
}
