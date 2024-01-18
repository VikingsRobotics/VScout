using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace VScoutCommon.Common
{
    public class FRCWebApiClient : IDisposable
    {
        HttpClient _httpClient;

        private static class Constants
        {
            public const string UserName = "jcheadle";
            public const string Token = "E0029612-F61A-4500-9D31-A84419C32041";
        }

        public FRCWebApiClient()
        {
            string encodedToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Constants.UserName}:{Constants.Token}"));

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", encodedToken);
        }

        public string Get(string endpoint)
        {
            HttpResponseMessage response = _httpClient.GetAsync(endpoint).Result;
            response.EnsureSuccessStatusCode();
            string stringResult = Encoding.Default.GetString(response.Content.ReadAsByteArrayAsync().Result);
            return stringResult;
        }

        public T Get<T>(string endpoint)
        {
            HttpResponseMessage response = _httpClient.GetAsync(endpoint).Result;
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
                    _httpClient.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~FRCWebApiClient()
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
