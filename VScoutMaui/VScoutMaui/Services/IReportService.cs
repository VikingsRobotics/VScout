namespace VScoutCentral.Services
{
    public interface IReportService
    {
        Task<string> GetAllRoundsDataCsv();
    }
}