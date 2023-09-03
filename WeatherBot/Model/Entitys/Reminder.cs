namespace WeatherBot.Model.Entitys
{
    public class Reminder : BaseEntity
    {
        public long UserId { get; set; }
        public TimeOnly ReminberTime { get; set; }
        public int CityId { get; set; }
    }
}
