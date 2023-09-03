using Telegram.Bot.Types.ReplyMarkups;

namespace WeatherBot.Model.Helper
{
    public class KeyboardButtons
    {
        public static readonly ReplyKeyboardMarkup RequestGeolocation = new(new[]
        {
            new[]
            {
                KeyboardButton.WithRequestLocation(ButtonText.RequestGeolocalion)
            }
        }
        ){ ResizeKeyboard = true };

        public static readonly ReplyKeyboardRemove RemoveButtons = new ReplyKeyboardRemove();
    }
}
