using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VScoutCommon.Common
{
    public class BlueAllianceWebApiClient : IDisposable
    {
        HttpClient httpClient;

        private static class Constants
        {
            public const string Token = "cyExOuXEggqKQOdV2QfVOlHlq8fcmTrYcMMn5EMTNH5R41uCZct3dPbepdCqcRoq";
        }

        public BlueAllianceWebApiClient()
        {
            this.httpClient = new HttpClient();
            this.httpClient.DefaultRequestHeaders.Add("X-TBA-Auth-Key", Constants.Token);
        }

        public string Get(string endpoint)
        {
            HttpResponseMessage response = this.httpClient.GetAsync(endpoint).Result;
            response.EnsureSuccessStatusCode();
            string stringResult = Encoding.Default.GetString(response.Content.ReadAsByteArrayAsync().Result);
            return stringResult;
        }

        public T Get<T>(string endpoint)
        {
            HttpResponseMessage response = this.httpClient.GetAsync(endpoint).Result;
            response.EnsureSuccessStatusCode();
            string stringResult = Encoding.Default.GetString(response.Content.ReadAsByteArrayAsync().Result);

            return JsonConvert.DeserializeObject<T>(stringResult);
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
                    this.httpClient.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~BlueAllianceWebApiClient()
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
