using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherBot.Model.Entitys;
using WeatherBot.Model.Helper;
using WeatherBot.Model.Services.Interfaces;

namespace WeatherBot.Model.Command
{
    public class CreateNewUserCommand : ICommand
    {
        public string CommandName => CommandNames.Empty;

        private readonly IUserService userService;
        private readonly TelegramBotClient botClient;
        private readonly IGeoService geoService;

        public CreateNewUserCommand(IServiceProvider serviceProvider)
        {
            botClient = serviceProvider.GetRequiredService<TelegramBotClientManager>().GetBotClientAsync().Result;
            userService = serviceProvider.GetRequiredService<IUserService>();
            geoService = serviceProvider.GetRequiredService<IGeoService>();
        }

        /// <summary>
        /// Выполняет регистрацию пользователя: по заданным координатам получаем город пользователя,
        /// затем создается новый объект типа AppUser с указанным городом,
        /// новый пользователь заносится в базу.
        /// </summary>
        public async Task Execute(Update update)
        {
            var location = update?.Message?.Location;
            var chat = TelegramDataHelper.GetChat(update);
            var message = TelegramDataHelper.GetMessage(update);

            if (await userService.GetUserAsync(chat.Id) != null)
                return;

            await StartCreateUserAsync(location, chat, message);
        }

        private async Task StartCreateUserAsync(Location location, Chat chat, Message message)
        {
            var city = await geoService.GetCityAsync(location);

            if (city == null)
            {
                await botClient.SendTextMessageAsync(chat.Id, Messages.FailedGetGeolocation, replyToMessageId: message.MessageId);
                return;
            }
            else
            {
                await botClient.SendTextMessageAsync(chat.Id, Messages.SuccessGetGeolocation, replyToMessageId: message.MessageId, replyMarkup: KeyboardButtons.RemoveButtons);

                var user = await CreateUserAsync(message, city);
                var isSuccess = await userService.AddUserToDbAsync(user);

                await SendBackMessageToUser(chat, message, isSuccess);
            }
        }

        private async Task<AppUser> CreateUserAsync(Message message, City city)
        {
            var user = new AppUser()
            {
                ChatId = message.Chat.Id,
                Name = message?.From?.Username,
                TimeZone = await geoService.GetTimeZoneAsync(city),
                CityId = city.Id,
                SubscribeMailing = true
            };

            return user;
        }

        private async Task SendBackMessageToUser(Chat chat, Message message, bool isSuccess)
        {
            await botClient.DeleteMessageAsync(chat.Id, message.MessageId - 1);

            if (isSuccess)
            {
                await botClient.SendTextMessageAsync(chat.Id, Messages.RegistrationSuccess);
            }
            else
            {
                await botClient.SendTextMessageAsync(chat.Id, Messages.RegistrationFailed);
            }
        }

    }
}
