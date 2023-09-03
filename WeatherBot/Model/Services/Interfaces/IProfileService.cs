using Telegram.Bot.Types;

namespace WeatherBot.Model.Services.Interfaces
{
    public interface IProfileService
    {
        public Task<bool> EditNameAsync(Update update, string newName);
        public Task<bool> AddReminderAsync(Update update);
        public Task<bool> RemoveAccountAsync(Update update);
    }
}
