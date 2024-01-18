using CommunityToolkit.Mvvm.ComponentModel;
using Joe.Common;
using VScoutCentral.Models;
using VScoutCentral.Services;

namespace VScoutCentral.ViewModels
{
    public partial class ReadQRCodeViewModel : ObservableObject
    {
        private readonly IScheduleDatabaseService _scheduleDatabaseService;

        public ReadQRCodeViewModel(IScheduleDatabaseService scheduleDatabaseService)
        {
            _scheduleDatabaseService = scheduleDatabaseService;
        }

        public async Task ProcessQRCodeAsync(string qrCodeValue)
        {
            try
            {
                List<TeamRound> teamRounds = GetTeamRoundsFromString(qrCodeValue);

                foreach (TeamRound teamRound in teamRounds)
                {
                    await _scheduleDatabaseService.SaveTeamRoundAsync(teamRound);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        private static List<TeamRound> GetTeamRoundsFromString(string qrCodeValue)
        {
            string[] roundStrings = qrCodeValue.Split('$', StringSplitOptions.RemoveEmptyEntries);

            List<TeamRound> teamRounds = new List<TeamRound>(roundStrings.Length);

            foreach (string roundString in roundStrings)
            {
                teamRounds.Add(GetTeamRoundFromString(roundString));
            }

            return teamRounds;
        }

        private static TeamRound GetTeamRoundFromString(string qrCodeValue)
        {
            try
            {
                string[] parts = qrCodeValue.Split(new char[] { '*' });

                int tempMatchNumber = JoeConvert.ToInt32(parts[0]);
                int tempTeamNumber = JoeConvert.ToInt32(parts[1]);
                bool tempAutoMoved = JoeConvert.ToBoolean(parts[2]);
                bool tempAutoDocked = JoeConvert.ToBoolean(parts[3]);
                bool tempAutoEngaged = JoeConvert.ToBoolean(parts[4]);
                int tempAutoConeHigh = JoeConvert.ToInt32(parts[5]);
                int tempAutoConeMiddle = JoeConvert.ToInt32(parts[6]);
                int tempAutoConeLow = JoeConvert.ToInt32(parts[7]);
                int tempAutoCubeHigh = JoeConvert.ToInt32(parts[8]);
                int tempAutoCubeMiddle = JoeConvert.ToInt32(parts[9]);
                int tempAutoCubeLow = JoeConvert.ToInt32(parts[10]);
                bool tempDocked = JoeConvert.ToBoolean(parts[11]);
                bool tempEngaged = JoeConvert.ToBoolean(parts[12]);
                int tempConeHigh = JoeConvert.ToInt32(parts[13]);
                int tempConeMiddle = JoeConvert.ToInt32(parts[14]);
                int tempConeLow = JoeConvert.ToInt32(parts[15]);
                int tempCubeHigh = JoeConvert.ToInt32(parts[16]);
                int tempCubeMiddle = JoeConvert.ToInt32(parts[17]);
                int tempCubeLow = JoeConvert.ToInt32(parts[18]);

                return new TeamRound
                {
                    MatchNumber = JoeConvert.ToInt32(parts[0]),
                    TeamNumber = JoeConvert.ToInt32(parts[1]),
                    AutoMoved = JoeConvert.ToBoolean(parts[2]),
                    AutoDocked = JoeConvert.ToBoolean(parts[3]),
                    AutoEngaged = JoeConvert.ToBoolean(parts[4]),
                    AutoConeHigh = JoeConvert.ToInt32(parts[5]),
                    AutoConeMiddle = JoeConvert.ToInt32(parts[6]),
                    AutoConeLow = JoeConvert.ToInt32(parts[7]),
                    AutoCubeHigh = JoeConvert.ToInt32(parts[8]),
                    AutoCubeMiddle = JoeConvert.ToInt32(parts[9]),
                    AutoCubeLow = JoeConvert.ToInt32(parts[10]),
                    Docked = JoeConvert.ToBoolean(parts[11]),
                    Engaged = JoeConvert.ToBoolean(parts[12]),
                    ConeHigh = JoeConvert.ToInt32(parts[13]),
                    ConeMiddle = JoeConvert.ToInt32(parts[14]),
                    ConeLow = JoeConvert.ToInt32(parts[15]),
                    CubeHigh = JoeConvert.ToInt32(parts[16]),
                    CubeMiddle = JoeConvert.ToInt32(parts[17]),
                    CubeLow = JoeConvert.ToInt32(parts[18]),
                    Notes = JoeConvert.ToString(parts[19])
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
