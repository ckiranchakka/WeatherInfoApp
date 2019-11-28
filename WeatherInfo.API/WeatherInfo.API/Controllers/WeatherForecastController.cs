using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherInfo.Services;
using wheatherInfo.Utilities;
using WeatherInfo.Entities;
namespace WeatherInfo.API.Controllers
{

    /// <summary>
    /// WeatherForecastController
    /// </summary>
    [Route("api/weatherforecast")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {

        #region Constructor
        private readonly IWeatherForecastService weatherService;
        private readonly ExceptionHelper exceptionHelper;
        /// <summary>
        /// WeatherForecastController
        /// </summary>
        /// <param name="_weatherService"></param>
        /// <param name="_exceptionHelper"></param>
        public WeatherForecastController(IWeatherForecastService _weatherService, ExceptionHelper _exceptionHelper)
        {
            weatherService = _weatherService;
            exceptionHelper = _exceptionHelper;
        }
        #endregion
        /// <summary>
        /// Gets the weather forecast.
        /// </summary>
        ///<param name="searchCriteria"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(WeatherForecastDetails), 200)]
        [HttpGet("search")]
        public async Task<IActionResult> GetWeatherForecast([FromQuery] SearchCriteria searchCriteria)
        {
            try
            {
                var result = await weatherService.GetWeatherForecast(searchCriteria);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return exceptionHelper.GetResponseStatus(ex);
            }
        }
        //private static readonly string[] Summaries = new[]
        //{
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};

        //private readonly ILogger<WeatherForecastController> _logger;

        //public WeatherForecastController(ILogger<WeatherForecastController> logger)
        //{
        //    _logger = logger;
        //}

        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}




        ///// <summary>
        ///// Gets the agencies.
        ///// </summary>
        /////<param name="agencyId"></param>
        ///// <returns></returns>
        //[ProducesResponseType(typeof(AgencyViewModel), 200)]
        //[HttpGet("{agencyId}")]
        //public async Task<IActionResult> GetWeatherForecast(int agencyId)
        //{
        //    try
        //    {
        //        var result = await agencyService.GetWeatherForecast(agencyId);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return exceptionHelper.GetResponseStatus(ex);
        //        //return exceptionHelper.GetResponseStatus(ex);
        //    }
        //}
    }
}
