using Telegram.Bot.Types;
using WeatherBot.Model.Entitys;

namespace WeatherBot.Model.Services.Interfaces
{
    public interface IGeoService
    {
        /// <summary>
        /// Возвращает город из баззы данных по указанному id.
        /// </summary>
        /// <param name="id">ID города.</param>
        /// <returns>Искомый город.</returns>
        public Task<City> GetCityAsync(long id);

        /// <summary>
        /// Пытается найти город с указанными координатами в базе данных, если такого города там нет,
        /// то делается запрос к API и создается новый экземпляр города.
        /// </summary>
        /// <param name="location">Локация пользователя.</param>
        /// <returns>Возвращает искомый город. Если город обнаружить не удалось, возвращает null.</returns>
        public Task<City?> GetCityAsync(Location location);

        /// <summary>
        /// Делает запрос к API и возвращает часовой пояс города.
        /// </summary>
        /// <returns>Часовой пояс формата: "страна/город"</returns>
        public Task<string?> GetTimeZoneAsync(City city);
    }
}
