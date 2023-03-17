using Infrastructuur.Data.Interfaces;
using Infrastructuur.Dtos;
using Infrastructuur.Entities;
using Infrastructuur.Extensions;
using Newtonsoft.Json;

namespace Infrastructuur.Data.Classes
{
    public class WeatherData : IWeaterData
    {
        private readonly string apiKey = ApiKeyReader.KeyReader(@"C:/Users/louag/source/repos/keyWeatherforecast/WeatherForecastKey.json").Key;
        private readonly string baseUrl = "https://api.openweathermap.org/data/2.5";


        public async Task<ResultDto<CurrentWeatherResponse>> GetCurrentWeatherByCity(string city)
        {
            var resultDto = new ResultDto<CurrentWeatherResponse>();
            string cacheKey = $"weather-{city}";
            string url = $"{baseUrl}/weather?q={city}&appid={apiKey}&units=metric";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    CurrentWeatherResponse result = JsonConvert.DeserializeObject<CurrentWeatherResponse>(json);
                    if (result != null)
                    {
                        resultDto.Result?.Add(result);
                    }
                }
                else
                {
                    resultDto.Errors.Add("No data found!");
                }
            }
            return resultDto;
        }

        public async Task<ResultDto<ForecastWeatherResponse>> GetSevenDaysForecastWeather(string city)
        {
            var resultDto = new ResultDto<ForecastWeatherResponse>();

            string url = $"{baseUrl}/forecast?q={city}&appid={apiKey}&units=metric";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    ForecastWeatherResponse result = JsonConvert.DeserializeObject<ForecastWeatherResponse>(json);
                    if (result != null)
                    {
                        resultDto.Result.Add(result);
                    }
                }
                else
                {
                    resultDto.Errors.Add("No data found!");
                }
            }
            return resultDto;
        }
    }
}
