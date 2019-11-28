using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WeatherInfo.RequestHandlers
{
    public interface IHttpHandler
    {
        Task<T> GetRequest<T>(string uri, string accessToken);
        Task<TOut> PostRequest<TIn, TOut>(string uri, TIn content, string accessToken);
        Task<TOut> PutRequest<TIn, TOut>(string uri, TIn content, string accessToken);
        Task<T> DeleteRequest<T>(string uri, string accessToken);
    }
}
