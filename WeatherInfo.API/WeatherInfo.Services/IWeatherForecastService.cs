using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherInfo.Entities;
namespace WeatherInfo.Services
{
  public  interface IWeatherForecastService
    {
        Task<WeatherViewModel> GetWeatherForecast(SearchCriteria searchCriteria);
    }
}
