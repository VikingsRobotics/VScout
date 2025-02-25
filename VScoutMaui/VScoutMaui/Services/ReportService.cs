using System.Text;

namespace VScoutCentral.Services
{
    public class ReportService : IReportService
    {
        private readonly IScheduleDatabaseService _scheduleDatabaseService;

        public ReportService(IScheduleDatabaseService scheduleDatabaseService)
        {
            _scheduleDatabaseService = scheduleDatabaseService;
        }

        public async Task<string> GetAllRoundsDataCsv()
        {
            List<Models.TeamRound> teamRounds = await _scheduleDatabaseService.GetTeamRoundsAsync();

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("TeamNumber,MatchNumber,Rank,Wins,Losses,Ccwms,Dprs,Oprs,AutonomousMoved,AutonomousDocked,AutonomousEngaged,AutonomousConeHigh,AutonomousConeMiddle,AutonomousConeBottom,AutonomousCubeHigh,AutonomousCubeMiddle,AutonomousCubeBottom,ConeHigh,ConeMiddle,ConeBottom,CubeHigh,CubeMiddle,CubeBottom,Docked,Engaged,Comments,HasData\r\n");

            foreach (Models.TeamRound teamRound in teamRounds)
            {
                string notes = teamRound.Notes.Replace('\r', ' ').Replace('\n', ' ').Replace(',', ';');

                stringBuilder.Append($"{teamRound.TeamNumber},{teamRound.MatchNumber},{teamRound.Rank},{teamRound.Wins},{teamRound.Losses}," +
                   $"{teamRound.Ccwm},{teamRound.Dpr},{teamRound.Opr}," +
                   $"{teamRound.AutoMoved},{teamRound.AutoAmp},{teamRound.AutoSpeaker}," +
                   $"{teamRound.Amp},{teamRound.Speaker},{teamRound.OnStage}," +
                   $"{teamRound.OnStageChain},{teamRound.NoteInOnChain},{teamRound.Spotlight}," +
                   $"{notes},{teamRound.HasData}\r\n");
            }

            return stringBuilder.ToString();
        }
    }
}
