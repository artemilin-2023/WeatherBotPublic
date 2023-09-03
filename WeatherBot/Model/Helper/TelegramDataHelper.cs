using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace WeatherBot.Model.Helper
{
    public static class TelegramDataHelper
    {
        public static Chat? GetChat(Update update)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    return update?.Message?.Chat;

                case UpdateType.CallbackQuery:
                    return update?.CallbackQuery?.Message?.Chat;
            }
            return null;
        }

        public static Message? GetMessage(Update update)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    return update?.Message;

                case UpdateType.CallbackQuery:
                    return update?.CallbackQuery?.Message;
            }

            return null;
        }

        public static string? GetCommandText(Update update)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    return update?.Message?.Text;

                case UpdateType.CallbackQuery:
                    return update?.CallbackQuery?.Data;
            }

            return null;
        }
    }
}
