using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherBot.Model.Helper;

namespace WeatherBot.Model.Command
{
    public class RefuseRegistrationCommand : ICommand
    {
        public string CommandName { get => CommandNames.RefuseRegistrationCommand; }

        private readonly TelegramBotClient botClient;

        public RefuseRegistrationCommand(IServiceProvider serviceProvider)
        {
            botClient = serviceProvider.GetRequiredService<TelegramBotClientManager>().GetBotClientAsync().Result;
        }

        public async Task Execute(Update update)
        {
            var chat = TelegramDataHelper.GetChat(update);
            var message = TelegramDataHelper.GetMessage(update);

            await botClient.DeleteMessageAsync(chat.Id, message.MessageId);
            await botClient.SendTextMessageAsync(chat.Id, Messages.RefuseRegistration);
        }
    }
}
