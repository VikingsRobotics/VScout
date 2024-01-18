using System;
using System.Collections.Generic;
using System.Linq;
using VScoutCommon.Models;

namespace VScoutCommon.Common
{
    public class FRCClient : IDisposable
    {
        FRCWebApiClient _frcWebApiClient = new FRCWebApiClient();

        private readonly string EventCode;
        private readonly int Year;

        public FRCClient(string eventCode, int year)
        {
            this.EventCode = eventCode;
            this.Year = year;
        }

        public Schedule GetSchedule(int team)
        {
            return _frcWebApiClient.Get<Schedule>($"https://frc-api.firstinspires.org/v2.0/{this.Year}/schedule/{this.EventCode}?tournamentLevel=qual&teamNumber={team}");
        }

        public Schedule GetSchedule()
        {
            return _frcWebApiClient.Get<Schedule>($"https://frc-api.firstinspires.org/v2.0/{this.Year}/schedule/{this.EventCode}?tournamentLevel=qual");
        }

        public List<Ranking> GetRankings()
        {
            GetRankingsResult result = _frcWebApiClient.Get<GetRankingsResult>($"https://frc-api.firstinspires.org/v2.0/{this.Year}/rankings/{this.EventCode}");

            return result.Rankings.ToList();
        }

        public string GetRankingsString()
        {
            return _frcWebApiClient.Get($"https://frc-api.firstinspires.org/v2.0/{this.Year}/rankings/{this.EventCode}");
        }

        public string GetScheduleFileContents()
        {
            return _frcWebApiClient.Get($"https://frc-api.firstinspires.org/v2.0/{this.Year}/schedule/{this.EventCode}?tournamentLevel=qual");
        }

        public List<Team> GetTeams()
        {
            GetTeamsResult result = _frcWebApiClient.Get<GetTeamsResult>($"https://frc-api.firstinspires.org/v2.0/{this.Year}/teams?eventCode={this.EventCode}");

            return result.Teams.ToList();
        }

        public void Test()
        {
            // mndu2
            string obj = _frcWebApiClient.Get("https://frc-api.firstinspires.org/v2.0/2019/events?teamNumber=4182");
            //object obj = _frcWebApiClient.Get<object>("https://frc-api.firstinspires.org/v2.0/2019/schedule/mndu2?tournamentLevel=qual");
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _frcWebApiClient.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~FRCClient()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
