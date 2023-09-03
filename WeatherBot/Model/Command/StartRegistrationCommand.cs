using Microsoft.AspNetCore.Session;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherBot.Model.Entitys;
using WeatherBot.Model.Helper;

namespace WeatherBot.Model.Command
{
    public class StartRegistrationCommand : ICommand
    {
        public string CommandName { get => CommandNames.StartRegistrationCommand; }

        private readonly TelegramBotClient botClient;

        public StartRegistrationCommand(IServiceProvider provider)
        {
            botClient = provider.GetRequiredService<TelegramBotClientManager>().GetBotClientAsync().Result;
        }

        public async Task Execute(Update update)
        {
            var chat = TelegramDataHelper.GetChat(update);
            var message = TelegramDataHelper.GetMessage(update);

            await botClient.DeleteMessageAsync(chat.Id, message.MessageId);
            await botClient.SendTextMessageAsync(chat.Id, Messages.RequestGeolocalion, replyMarkup: KeyboardButtons.RequestGeolocation);

        }
    }
}
