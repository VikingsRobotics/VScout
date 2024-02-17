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

                return new TeamRound
                {
                    MatchNumber = JoeConvert.ToInt32(parts[0]),
                    TeamNumber = JoeConvert.ToInt32(parts[1]),
                    AutoMoved = JoeConvert.ToBoolean(parts[2]),
                    AutoAmp = JoeConvert.ToInt32(parts[3]),
                    AutoSpeaker = JoeConvert.ToInt32(parts[4]),
                    Amp = JoeConvert.ToInt32(parts[5]),
                    Speaker = JoeConvert.ToInt32(parts[6]),
                    OnStage = JoeConvert.ToBoolean(parts[7]),
                    OnStageChain = JoeConvert.ToBoolean(parts[8]),
                    NoteInOnChain = JoeConvert.ToBoolean(parts[9]),
                    Spotlight = JoeConvert.ToBoolean(parts[10]),
                    Notes = JoeConvert.ToString(parts[11])
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
