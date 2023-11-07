namespace BookStore.Services.Interfaces
{
    public interface IWeatherForecast
    {
        IEnumerable<WeatherForecast> GetReadouts();
    }
}
