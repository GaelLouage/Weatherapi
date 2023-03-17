namespace Infrastructuur.Entities
{
    public class WeatherForecast
    {
        public Main? main { get; set; }
        public Weather[]? weather { get; set; }
        public DateTime? dt_txt { get; set; }
    }
}
