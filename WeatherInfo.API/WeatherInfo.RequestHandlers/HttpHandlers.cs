using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WeatherInfo.RequestHandlers
{
    public class HttpHandlers : IHttpHandler
    {
        private readonly HttpClient client;

        public HttpHandlers(HttpClient client)
        {
            this.client = client;
        }
        /// <summary>
        ///Get Api calls
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri">Request url</param>
        /// <param name="accessToken">Token for authentication</param>
        /// <returns></returns>
        public async Task<T> GetRequest<T>(string uri, string accessToken)
        {
            try
            {

                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                using (HttpResponseMessage response = await client.GetAsync(uri))
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(responseBody);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Post Api calls
        /// </summary>
        /// <typeparam name="TIn">Request body</typeparam>
        /// <typeparam name="TOut">Response</typeparam>
        /// <param name="uri">Request url</param>
        /// <param name="content">Request body</param>
        /// <param name="accessToken">Token for authentication</param>
        /// <returns></returns>
        public async Task<TOut> PostRequest<TIn, TOut>(string uri, TIn content, string accessToken)
        {
            try
            {
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                
                var serialized = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PostAsync(uri, serialized))
                {
                    // response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TOut>(responseBody);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Put Api calls
        /// </summary>
        /// <typeparam name="TIn">Request body</typeparam>
        /// <typeparam name="TOut">Response</typeparam>
        /// <param name="uri">Request url</param>
        /// <param name="content">Request body</param>
        /// <param name="accessToken">Token for authentication</param>
        /// <returns></returns>
        public async Task<TOut> PutRequest<TIn, TOut>(string uri, TIn content, string accessToken)
        {
            try
            {
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
               
                var serialized = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PutAsync(uri, serialized))
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TOut>(responseBody);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Delete Api calls
        /// </summary>
        /// <typeparam name="T">Response</typeparam>
        /// <param name="uri">Request url</param>
        /// <param name="accessToken">Token for authentication</param>
        /// <returns></returns>
        public async Task<T> DeleteRequest<T>(string uri, string accessToken)
        {
            try
            {
             
                var response = await client.DeleteAsync(uri).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    response.Content?.Dispose();
                    throw new HttpRequestException($"{response.StatusCode}:{content}");
                }
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseBody);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
