using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherInfo.Entities;
using WeatherInfo.Entities.Constants;
using WeatherInfo.RequestHandlers;
namespace WeatherInfo.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IHttpHandler httpHandler;
        public WeatherForecastService(IHttpHandler _httpHandler)
        {
            httpHandler = _httpHandler;

        }
        public async Task<WeatherViewModel> GetWeatherForecast(SearchCriteria searchCriteria)
        {
            try
            {
                EntityModelConverter entityModel = new EntityModelConverter();

                var apiUrl = string.Empty;

                if (!string.IsNullOrEmpty(searchCriteria.ZipCode))
                    apiUrl = APIConstants.API_URL + "?zip=" + searchCriteria.ZipCode + "&APPID=" + APIConstants.API_KEY;
                else
                    apiUrl = APIConstants.API_URL + "?q=" + searchCriteria.City + "&APPID=" + APIConstants.API_KEY;
                var entityResult = httpHandler.GetRequest<WeatherForecastDetails>(apiUrl, string.Empty);
                
                return await Task.FromResult(entityModel.WeatherEntityModelConverter(entityResult));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

       
    }
}
