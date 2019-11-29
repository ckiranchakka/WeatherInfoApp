using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace WeatherInfo.Entities
{
    public class EntityModelConverter
    {
        /// <summary>
        /// Transform Weather Forecast Details to Weather ViewModel.
        /// </summary>
        /// <param name="entityResult"></param>
        /// <returns></returns>
        public WeatherViewModel WeatherEntityModelConverter(Task<WeatherForecastDetails> entityResult)
        {
            try
            {
                //if (entityResult.Status !=TaskStatus.RanToCompletion)
                //    return new WeatherViewModel();
                return new WeatherViewModel
                {

                    City = entityResult.Result.city?.name,
                    Country = entityResult.Result.city?.country,
                    forecasts = getForeCasts(entityResult.Result)


                };
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// getForeCasts
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private static List<Forecast> getForeCasts(WeatherForecastDetails result)
        {
            try
            {
                List<Forecast> forecasts = new List<Forecast>();
                foreach (var wforecast in result.list)
                {
                    bool dateExists = forecasts.Any(x => x.Date.ToString() == Convert.ToDateTime(wforecast.dt_txt).ToShortDateString());
                    if (!dateExists)
                    {
                        Forecast forecast = new Forecast();
                        forecast.Date = Convert.ToDateTime(wforecast.dt_txt).ToShortDateString();
                        forecast.Date_time = Convert.ToDateTime(wforecast.dt_txt);
                        forecast.Day = Convert.ToDateTime(wforecast.dt_txt).DayOfWeek.ToString();
                        forecast.Pressure = Convert.ToDouble(wforecast.main.pressure);
                        forecast.Humdity = Convert.ToDouble(wforecast.main.humidity);
                        forecast.Speed = wforecast.wind.speed;
                        forecast.Icon = Constants.APIConstants.ICON_URL + wforecast.weather[0].icon + "@2x.png";
                        forecast.Description = wforecast.weather[0].description;
                        forecast.WeatherType = wforecast.weather[0].main;
                        forecast.Temperature = new Temperature(wforecast.main.temp,
                        wforecast.main.temp_min, wforecast.main.temp_max);
                        forecast.Periods = new List<Dayparts>();
                        forecast.Periods.Add(getDayParts(wforecast));
                        forecasts.Add(forecast);
                    }
                    else
                    {
                        Forecast fcast = (from a in forecasts
                                          where a.Date.ToString() == Convert.ToDateTime(wforecast.dt_txt).ToShortDateString()
                                          select a).SingleOrDefault();
                        fcast.Periods.Add(getDayParts(wforecast));
                    }

                }

                return forecasts;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private static Dayparts getDayParts(List wforecast)
        {
            Dayparts dayparts = new Dayparts();
            dayparts.Date = Convert.ToDateTime(wforecast.dt_txt).ToShortDateString();
            dayparts.Date_time = Convert.ToDateTime(wforecast.dt_txt);
            dayparts.Day = Convert.ToDateTime(wforecast.dt_txt).DayOfWeek.ToString();
            dayparts.Date_time = Convert.ToDateTime(wforecast.dt_txt);
            dayparts.Pressure = Convert.ToDouble(wforecast.main.pressure);
            dayparts.Humdity = Convert.ToDouble(wforecast.main.humidity);
            dayparts.Speed = wforecast.wind.speed;
            dayparts.Icon = Constants.APIConstants.ICON_URL + wforecast.weather[0].icon + "@2x.png";
            dayparts.Description = wforecast.weather[0].description;
            dayparts.WeatherType = wforecast.weather[0].main;
            dayparts.temperature = new Temperature(wforecast.main.temp,
                                                    wforecast.main.temp_min, wforecast.main.temp_max);

            return dayparts;
        }
    }
}
