namespace VScoutCommon.Clients.BlueAllianceClient.DomainModels
{
    public class CalculatedStats
    {
        public required Dictionary<int, decimal> Oprs { get; init; }

        public required Dictionary<int, decimal> Dprs { get; init; }

        public required Dictionary<int, decimal> Ccwms { get; init; }
    }
}