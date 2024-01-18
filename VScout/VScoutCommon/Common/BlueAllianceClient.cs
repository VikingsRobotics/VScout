using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VScoutCommon.Common
{
    public class BlueAllianceClient : IDisposable
    {
        BlueAllianceWebApiClient blueAllianceWebApiClient = new BlueAllianceWebApiClient();

        private readonly string EventCode; // Should be of the form YYYY[eventkey] ex: 2020mndu2 (is case-sensitive)
        private readonly int Year;

        public BlueAllianceClient(string eventCode, int year)
        {
            this.EventCode = eventCode;
            this.Year = year;
        }

        public string GetOprsString()
        {
            return this.blueAllianceWebApiClient.Get($"https://www.thebluealliance.com/api/v3/event/{this.Year + this.EventCode.ToLower()}/oprs");
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~BlueAllianceClient()
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
