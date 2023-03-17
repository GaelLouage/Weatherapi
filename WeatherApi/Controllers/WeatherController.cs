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
        // your api key
        private readonly string apiKey = ApiKeyReader.KeyReader(@"C:/Users/louag/source/repos/keyWeatherforecast/WeatherForecastKey.json").Key;
        private readonly string baseUrl = "https://api.openweathermap.org/data/2.5";
        private readonly IMemoryCache _cache;

        public WeatherController(IMemoryCache cache)
        {
            this._cache = cache;
        }

        [HttpGet("current/{city}")]
        public async Task<ActionResult<CurrentWeatherResponse>> GetCurrentWeather(string city)
        {
            string cacheKey = $"weather-{city}";
            if (_cache.TryGetValue(cacheKey, out CurrentWeatherResponse cachedResult))
            {
                return Ok(cachedResult);
            }

            string url = $"{baseUrl}/weather?q={city}&appid={apiKey}&units=metric";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    CurrentWeatherResponse result = JsonConvert.DeserializeObject<CurrentWeatherResponse>(json);
                    // cache lives for 10 minutes
                    _cache.Set(cacheKey, result, TimeSpan.FromMinutes(10));
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
        }
        [HttpGet("forecast/{city}")]
        public async Task<ActionResult<ForecastWeatherResponse>> GetForecastWeather(string city)
        {
            if (_cache.TryGetValue(city, out ForecastWeatherResponse cachedResponse))
            {
                return Ok(cachedResponse);
            }
            else
            {
                string url = $"{baseUrl}/forecast?q={city}&appid={apiKey}&units=metric";
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        ForecastWeatherResponse result = JsonConvert.DeserializeObject<ForecastWeatherResponse>(json);
                        // cache lives for 10 minutes
                        _cache.Set(city, result, TimeSpan.FromMinutes(10));
                        return Ok(result);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
        }
    }
}
