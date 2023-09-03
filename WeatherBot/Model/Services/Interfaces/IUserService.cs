using Telegram.Bot.Types;
using WeatherBot.Model.Entitys;

namespace WeatherBot.Model.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Возвращает пользователя из базы данных.
        /// </summary>
        Task<AppUser?> GetUserAsync(long userId);

        /// <summary>
        /// Удаляет из базы данных пользователя и все связанные с ним записи. 
        /// </summary>
        /// <returns></returns>
        Task<bool> DeleteUserAsync(long userId);

        /// <summary>
        /// Меняет статус подписки на рассылку на противоположный.
        /// </summary>
        /// <returns>true - успешно, иначе false.</returns>
        Task<bool> ChangeSubscribeStatusAsync(long userId);

        /// <summary>
        /// Изменяет имя пользователя на новое.
        /// </summary>
        /// <returns>true - успешно, иначе false.</returns>
        Task<bool> EditNameAsync(long userId, string newName);

        /// <summary>
        /// Добавляет нового пользователя в базу данных.
        /// </summary>
        /// <returns>true - успешно, иначе false.</returns>
        Task<bool> AddUserToDbAsync(AppUser user);
    }
}
