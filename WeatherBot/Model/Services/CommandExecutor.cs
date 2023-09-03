using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using WeatherBot.Model.Command;
using WeatherBot.Model.Helper;

namespace WeatherBot.Model.Services
{
    public class CommandExecutor
    {
        private readonly IServiceProvider serviceProvider;

        private ICommand? currentCommand;

        public CommandExecutor(IServiceProvider provider) : base()
        {
            serviceProvider = provider;

            currentCommand = null;
        }

        public async Task StartExecution(Update update)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    ProcessTextMessage(update.Message);
                    break;

                case UpdateType.CallbackQuery:
                    ProcessCallbackQuery(update.CallbackQuery);
                    break;
            }

            if (currentCommand != null)
            {
                await currentCommand.Execute(update);

                currentCommand = null;
            }

        }

        private void ProcessCallbackQuery(CallbackQuery? callback)
        {
            switch (callback?.Data)
            {
                case CommandNames.StartRegistrationCommand:
                    currentCommand = new StartRegistrationCommand(serviceProvider);
                    break;

                case CommandNames.RefuseRegistrationCommand:
                    currentCommand = new RefuseRegistrationCommand(serviceProvider);
                    break;
            }
        }

        private void ProcessTextMessage(Message? message)
        {
            switch (message?.Text)
            {
                case CommandNames.Start:
                    currentCommand = new StartCommand(serviceProvider);
                    return;

                case CommandNames.Profile:
                    currentCommand = new GetProfileCommand(serviceProvider);
                    return;

                case CommandNames.Weather:
                    currentCommand = new GetCurrentWeatherCommand(serviceProvider);
                    return;
            }

            // Геолокация пользователя передается через тип Message, поэтому обработка происходит тут.
            if (message.Location != null)
            {
                currentCommand = new CreateNewUserCommand(serviceProvider);
            }
        }
    }
}
