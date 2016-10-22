using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MvvmCross.Plugins.Sqlite;
using SQLite.Net;
using SQLite.Net.Async;
using System;
using Glimpse.Core.Contracts.Repository;
using MvvmCross.Platform;

namespace Glimpse.Core.Repositories
{
    public class BaseRepository
    {

        //Database connection placed here temporarily for testing purposes... Should be used throughout the application...














        private HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        }

        protected async Task<T> GetAsync<T>(string url)
            where T : new()
        {
            HttpClient httpClient = CreateHttpClient();
            T result;

            try
            {
                var response = await httpClient.GetStringAsync(url);
                result = await Task.Run(() => JsonConvert.DeserializeObject<T>(response));
            }
            catch
            {
                result = new T();
            }

            return result;
        }
    }
}