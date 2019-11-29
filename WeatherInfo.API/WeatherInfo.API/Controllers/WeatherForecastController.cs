using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherInfo.Services;
using wheatherInfo.Utilities;
using WeatherInfo.Entities;
using Microsoft.Extensions.Caching.Memory;
using IMemoryCache = Microsoft.Extensions.Caching.Memory.IMemoryCache;

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
        private readonly IMemoryCache memoryCache;
        /// <summary>
        /// WeatherForecastController
        /// </summary>
        /// <param name="_weatherService"></param>
        /// <param name="_exceptionHelper"></param>
        public WeatherForecastController(IWeatherForecastService _weatherService, ExceptionHelper _exceptionHelper, IMemoryCache _memoryCache)
        {
            weatherService = _weatherService;
            exceptionHelper = _exceptionHelper;
            memoryCache = _memoryCache;
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
                WeatherViewModel weatherViewModel;

                if (searchCriteria == null || ((searchCriteria.isZipCode && string.IsNullOrEmpty(searchCriteria.ZipCode)) || (!searchCriteria.isZipCode && string.IsNullOrEmpty(searchCriteria.City))))
                    return BadRequest("Enter valid details");

                string searchText = string.Empty;
                //check for zipcode or city to filter from memory cache 
                if (searchCriteria.isZipCode)
                {
                    searchText = searchCriteria.ZipCode;
                }
                else
                {
                    searchText = searchCriteria.City;
                }

                //set into cache
                bool isExist = memoryCache.TryGetValue(searchText, out weatherViewModel);
                if (!isExist)
                {
                    weatherViewModel = await weatherService.GetWeatherForecast(searchCriteria);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(60));
                    memoryCache.Set(searchText, weatherViewModel, cacheEntryOptions);
                }
                else
                {
                    weatherViewModel = memoryCache.Get<WeatherViewModel>(searchText);
                }

                return Ok(weatherViewModel);
            }
            catch (Exception ex)
            {
                return exceptionHelper.GetResponseStatus(ex);
                //Log error details
            }
        }
    }
}
