using Infrastructuur.Data.Interfaces;
using Infrastructuur.Dtos;
using Infrastructuur.Entities;
using Infrastructuur.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace WeatherApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeaterData _weatherData;
        private readonly IMemoryCache _cache;

        public WeatherController(IMemoryCache cache, IWeaterData weatherData)
        {
            _cache = cache;
            _weatherData = weatherData;
        }
       
        [HttpGet("current/{city}")]
        public async Task<ActionResult<ResultDto<ForecastWeatherResponse>>> GetCurrentWeatherByName(string city)
        {
            string cacheKey = $"current-{city}";
            if (_cache.TryGetValue(cacheKey, out ResultDto<CurrentWeatherResponse> cachedResult))
            {
                return Ok(cachedResult);
            }
            var weather = await _weatherData.GetCurrentWeatherByCity(city);
            _cache.Set(cacheKey, weather, TimeSpan.FromMinutes(10));
            return Ok(weather);
        }
        [HttpGet("forecast/{city}")]
        public async Task<ActionResult<ForecastWeatherResponse>> GetSevenDaysForecastWeatherByCity(string city)
        {
            string cacheKey = $"forecast-{city}";
            if (_cache.TryGetValue(cacheKey, out ResultDto<ForecastWeatherResponse> cachedResult))
            {
                return Ok(cachedResult);
            }
            var weather = await _weatherData.GetSevenDaysForecastWeather(city);
            _cache.Set(cacheKey, weather, TimeSpan.FromMinutes(10));
            return Ok(weather);
        }
    }
}
