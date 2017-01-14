using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using System;
using System.Diagnostics;
using System.Net;

namespace Plugin.RestClient
{
    /// <summary>
    ///     RestClient implements methods for calling CRUD operations
    ///     using HTTP.
    /// </summary>
    public class RestClient<T> {

        // http://glimpsews.azurewebsites.net/api/ 
        // http://glimpseservices.azurewebsites.net/api/
        private readonly string WebServiceUrl = "http://10.0.3.2/Glimpse/api/" + typeof(T).Name + "s/";

        public async Task<List<T>> GetAsync()
        {
                var httpClient = new HttpClient();
                var json = await httpClient.GetStringAsync(WebServiceUrl);
                 var taskModels = JsonConvert.DeserializeObject<List<T>>(json);
            return taskModels;
        }


        public async Task<List<T>> GetByIdAsync(int id)
        {

                var httpClient = new HttpClient();

                var json = await httpClient.GetStringAsync(WebServiceUrl + id);

                var taskModels = JsonConvert.DeserializeObject<List<T>>(json);
            

            return taskModels;
        }
        public async Task<List<T>> GetWithFilter(string filter)
        {

            var httpClient = new HttpClient();

            var json = await httpClient.GetStringAsync(WebServiceUrl + "filter/" + filter);

            var taskModels = JsonConvert.DeserializeObject<List<T>>(json);

            return taskModels;
        }

        public async Task<T> GetByKeyword(string keyword, bool slashRequired = false)
        {

            var httpClient = new HttpClient();

            string request = WebServiceUrl + "Search/" + keyword;

            if (slashRequired)
                request = request + "/";

            var json = await httpClient.GetStringAsync(request);

            var taskModel = JsonConvert.DeserializeObject<T>(json);

            return taskModel;
        }


        //TO REMOVE
        public async Task<T> GetUserByEmailAsync(string email)
        {

            var httpClient = new HttpClient();

            var json = await httpClient.GetStringAsync(WebServiceUrl + "Search/" + email + "/");

            var taskModel = JsonConvert.DeserializeObject<T>(json);

            return taskModel;

        }

        public async Task<bool> PostAsync(T t)
        {
            var httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(t);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await httpClient.PostAsync(WebServiceUrl, httpContent);

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> PutAsync(int id, T t)
        {
            var httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(t);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await httpClient.PutAsync(WebServiceUrl + id, httpContent);

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id, T t)
        {
            var httpClient = new HttpClient();

            var response = await httpClient.DeleteAsync(WebServiceUrl + id);

            return response.IsSuccessStatusCode;
        }

        public async Task<int> GetIdAsync(string email)
        {

                var httpClient = new HttpClient();

                var json = await httpClient.GetStringAsync(WebServiceUrl + "Search/" + email + "/");

                var list = JsonConvert.DeserializeObject<List<dynamic>>(json);

                var  obj = list.FirstOrDefault();
                return (int)obj["Id"];
        }
    }
}
