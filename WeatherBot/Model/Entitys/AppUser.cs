using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherBot.Model.Entitys
{
    public class AppUser : BaseEntity
    {
        public long ChatId { get; set; }
        public string? Name { get; set; }
        public string? TimeZone { get; set; }
        public long CityId { get; set; }
        public bool SubscribeMailing { get; set; }
    }
}
