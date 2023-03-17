namespace Infrastructuur.Entities
{
    public class CurrentWeatherResponse
    {
        public Weather[]? weather { get; set; }
        public Main? main { get; set; }
        public Wind? wind { get; set; }
        public long? dt { get; set; }
        public string? name { get; set; }
    }
}
