namespace VScoutCommon.ResourceModels
{
    public class GetTeamsResult
    {
        public Team[]? teams { get; set; }

        public int teamCountTotal { get; set; }

        public int teamCountPage { get; set; }

        public int pageCurrent { get; set; }

        public int pageTotal { get; set; }
    }
}
