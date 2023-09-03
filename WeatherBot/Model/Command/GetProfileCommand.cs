using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherBot.Model.Helper;
using WeatherBot.Model.Services.Interfaces;

namespace WeatherBot.Model.Command
{
    public class GetProfileCommand : ICommand
    {
        public string CommandName { get => CommandNames.Profile; }

        private readonly IUserService userService;
        private readonly IGeoService geoService;
        private readonly TelegramBotClient botClient;

        public GetProfileCommand(IServiceProvider serviceProvider)
        {
            userService = serviceProvider.GetRequiredService<IUserService>();
            geoService = serviceProvider.GetRequiredService<IGeoService>();
            botClient = serviceProvider.GetRequiredService<TelegramBotClientManager>().GetBotClientAsync().Result;
        }

        /// <summary>
        /// Отправляет пользователю его профиль.
        /// </summary>
        public async Task Execute(Update update)
        {
            var chat = TelegramDataHelper.GetChat(update);

            var profileView = await BuildProfileView(chat);

            await botClient.SendTextMessageAsync(chat.Id, profileView, replyMarkup: InlineButtons.ProfileButtons);
        }

        private async Task<string> BuildProfileView(Chat chat)
        {
            var user = await userService.GetUserAsync(chat.Id);
            if (user == null)
                return string.Empty;

            var city = await geoService.GetCityAsync(user.CityId);

            return $"Профиль пользователя\n\nИмя: {user.Name}\n\nГород: {city.Name}, {city.State}\n\nРассылка: {MailingStatus(user.SubscribeMailing)}";
        }

        private string MailingStatus(bool status) => status ? "подключена" : "отключена";
    }
}
