using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Telegram.Bot.Types;
using WeatherBot.Model.Entitys;
using WeatherBot.Model.Services.Interfaces;

namespace WeatherBot.Model.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext database;

        public UserService(DataContext data)
        {
            database = data;
        }

        public async Task<bool> DeleteUserAsync(long userId)
        {
            if (await GetUserAsync(userId) == null)
                return false;

            var user = await GetUserAsync(userId);
            var reminders = database.Reminders.Where(x => x.UserId == user.Id).ToList();

            if (reminders.Count > 0)
            {
                foreach (var reminder in reminders)
                {
                    var cityToRemove = await database.Cities.SingleOrDefaultAsync(x => x.Id == reminder.CityId);
                    if (cityToRemove != null)
                    {
                        database.Cities.Remove(cityToRemove);
                    }

                    database.Reminders.Remove(reminder);
                }
            }

            database.Users.Remove(user);

            await database.SaveChangesAsync();
            return true;
        }

        public async Task<AppUser?> GetUserAsync(long userId)
        {
            return await database.Users.SingleOrDefaultAsync(x => x.ChatId == userId);
        }

        public async Task<bool> ChangeSubscribeStatusAsync(long userId)
        {
            var user = await GetUserAsync(userId);
            if (user == null)
                return false;
            
            user.SubscribeMailing = !user.SubscribeMailing;
            await database.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditNameAsync(long userId, string newName)
        {
            var user = await GetUserAsync(userId);
            if (user == null)
                return false;

            user.Name = newName;
            await database.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddUserToDbAsync(AppUser user)
        {
            try
            {
                await database.Users.AddAsync(user);
                await database.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return false;
            }
            return true;
        }
    }
}
