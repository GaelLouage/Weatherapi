namespace Infrastructuur.Entities
{
    public class ForecastWeatherResponse
    {
        public WeatherForecast[]? list { get; set; }
        public City? city { get; set; }
    }
}
