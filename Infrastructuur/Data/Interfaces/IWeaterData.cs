using Infrastructuur.Dtos;
using Infrastructuur.Entities;

namespace Infrastructuur.Data.Interfaces
{
    public interface IWeaterData
    {
        Task<ResultDto<CurrentWeatherResponse>> GetCurrentWeatherByCity(string city);
        Task<ResultDto<ForecastWeatherResponse>> GetSevenDaysForecastWeather(string city   );

    }
}