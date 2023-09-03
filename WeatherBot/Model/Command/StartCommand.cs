using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherBot.Model.Helper;
using WeatherBot.Model.Services.Interfaces;

namespace WeatherBot.Model.Command
{
    public class StartCommand : ICommand
    {
        public string CommandName { get => CommandNames.Start; }

        private readonly TelegramBotClient botClient;
        private readonly IUserService userService;

        public StartCommand(IServiceProvider serviceProvider)
        {
            botClient = serviceProvider.GetRequiredService<TelegramBotClientManager>().GetBotClientAsync().Result;
            userService = serviceProvider.GetRequiredService<IUserService>();
        }

        /// <summary>
        /// Запрашивает подтверждение начала регистрации.
        /// </summary>
        public async Task Execute(Update update)
        {
            var chat = TelegramDataHelper.GetChat(update);
            var user = await userService.GetUserAsync(chat.Id);

            if (user == null)
            {
                await botClient.SendTextMessageAsync(chat.Id, Messages.ConfirmRegistratingMassage, replyMarkup: InlineButtons.ConfirmRegistrutingButtons);
            }
            else
            {
                await botClient.SendTextMessageAsync(chat.Id, Messages.AlreadyRegistered);
            }
        }
    }
}
