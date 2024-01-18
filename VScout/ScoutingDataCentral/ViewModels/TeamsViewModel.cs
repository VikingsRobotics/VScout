using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VScoutCommon.Common;
using VScoutCommon.Models.BusinessModels;
using VScoutCommon.ViewModels;

namespace ScoutingDataCentral.ViewModels
{
    public class TeamsViewModel : BaseViewModel
    {
        private ObservableCollection<TeamViewModel> teams = new ObservableCollection<TeamViewModel>();
        public ObservableCollection<TeamViewModel> Teams
        {
            get
            {
                return this.teams;
            }

            set
            {
                this.teams = value;
                OnPropertyChanged(nameof(Teams));
            }
        }

        public void LoadTeams()
        {
            foreach (string directoryName in Directory.EnumerateDirectories(Constants.CentralBaseFilePath).Select(m => Path.GetFileName(m)))
            {
                int teamNumber = int.Parse(directoryName);
                TeamFileRecord2020 teamFile = TeamFile.Get(Constants.CentralBaseFilePath, teamNumber);

                if (teamFile == null) continue;

                this.Teams.Add(new TeamViewModel
                {
                    Rank = teamFile.Rank,
                    Opr = teamFile.Opr,
                    AveragePowerCellPoints = teamFile.AveragePowerCellPoints,
                    HangCount = teamFile.ClimbCount,
                    Name = teamFile.Name,
                    Number = teamFile.TeamNumber,
                    ParkCount = teamFile.BalanceCount,
                    PositionalControlCount = teamFile.PostionControlCount,
                    RotationalControlCount = teamFile.RotationControlCount
                });
            }
        }
    }
}
