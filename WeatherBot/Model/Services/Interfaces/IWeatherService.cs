using WeatherBot.Model.Entitys;

namespace WeatherBot.Model.Services.Interfaces
{
    public interface IWeatherService
    {
        /// <summary>
        /// Получает от API текущую погоду в указанном городе.
        /// </summary>
        Task<Weather?> GetCurrentWeatherAsync(City city);

        /// <summary>
        /// Запрашивает у API погоду на несколько дней вперед.
        /// </summary>
        /// <param name="daysForecast">На сколько дней запрашивать прогноз погоды.</param>
        Task<Weather?> GetForecastWeather(City city, int daysForecast);
    }
}
