using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace WeatherInfo.Entities
{
    public class EntityModelConverter
    {
        public WeatherViewModel WeatherEntityModelConverter(Task<WeatherForecastDetails> entityResult)
        {
            try
            {
                if (entityResult == null)
                    return new WeatherViewModel();
                return new WeatherViewModel
                {

                    City = entityResult.Result.city.name,
                    Country = entityResult.Result.city.country,
                    forecasts = getForecasts(entityResult.Result)


                };
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private static List<Forecast> getForecasts(WeatherForecastDetails result)
        {
            try
            {
                List<Forecast> forecasts = new List<Forecast>();
                foreach (var wforecast in result.list)
                {
                    bool dateExists = forecasts.Any(x => x.Date.ToString() == Convert.ToDateTime(wforecast.dt_txt).ToShortDateString().ToString());
                    if (!dateExists)
                    {
                        Forecast forecast = new Forecast();
                        forecast.Date = Convert.ToDateTime(wforecast.dt_txt.ToString()).ToShortDateString();
                        forecast.Date_time = Convert.ToDateTime(wforecast.dt_txt.ToString());
                        forecast.Day= Convert.ToDateTime(wforecast.dt_txt.ToString()).DayOfWeek.ToString();
                        forecast.Pressure = Convert.ToDouble(wforecast.main.pressure.ToString());
                        forecast.Humdity = Convert.ToDouble(wforecast.main.humidity.ToString());
                        forecast.Speed = Convert.ToDouble(wforecast.wind.speed);
                        forecast.Icon =  "http://openweathermap.org/img/wn/"+wforecast.weather[0].icon.ToString()+ "@2x.png";
                        forecast.Description = wforecast.weather[0].description.ToString();
                        forecast.WeatherType = wforecast.weather[0].main.ToString();
                        forecast.Temperature = new Temperature(double.Parse(wforecast.main.temp.ToString()),
                        double.Parse(wforecast.main.temp_min.ToString()), double.Parse(wforecast.main.temp_max.ToString()));

                        forecasts.Add(forecast);
                    }
                }

                return forecasts;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
