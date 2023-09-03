namespace WeatherBot.Model.Helper
{
    public static class Messages
    {
        public const string ConfirmRegistratingMassage = "Для дальнейшей работы сервиса необходимо пройти регистрацию.\n\nХотите продолжить?";
        public const string AlreadyRegistered = $"Вы уже зарегистрированы!\nЧтобы открыть профиль, воспользуйтесь командой: {CommandNames.Profile}";
        public const string RefuseRegistration = $"Вы отказались от регистрации.\nЧтобы перезапустить бота нажмите: {CommandNames.Start}";
        public const string RequestGeolocalion =  "Для того, чтобы бот мог отправлять вам погоду, " +
                                 $"необходима ваша геолокация.\n\nНажмите кнопку \"{ButtonText.RequestGeolocalion}\" для продолжения. Предварительно необходимо включить геолокацию на телефоне.";
        public const string SuccessGetGeolocation = "Спасибо, геолокация получена. Запускаю процесс регистрации, на это может понадобиться некоторое время...";
        public const string FailedGetGeolocation = "Упс! Что-то пошло не так. Отправте геолокацию вновь или повторите попытку позже.\n\nВажно: необходимо отправить точную геолокацию.";
        public const string RegistrationSuccess = $"Регистрация прошла успешно!\nЧтобы открыть профиль, нажмите: {CommandNames.Profile}";
        public const string RegistrationFailed = $"Не удалось зарегестрировать пользователя. Повторите попытку позже.";
    }
}
