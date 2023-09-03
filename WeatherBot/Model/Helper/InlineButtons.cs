using Telegram.Bot.Types.ReplyMarkups;

namespace WeatherBot.Model.Helper
{
    public static class InlineButtons
    {
        public static readonly InlineKeyboardMarkup ProfileButtons = new(new[]
        {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(ButtonText.Settings, CommandNames.ProfileSettings),
                    InlineKeyboardButton.WithCallbackData(ButtonText.MyReminders, CommandNames.Reminders)
                },
        });

        public static readonly InlineKeyboardMarkup ConfirmRegistrutingButtons = new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData(ButtonText.No, CommandNames.RefuseRegistrationCommand),
                InlineKeyboardButton.WithCallbackData(ButtonText.Yes, CommandNames.StartRegistrationCommand)                
            }
        });
    }
}
