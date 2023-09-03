using System.Text.Json.Serialization;

namespace WeatherBot.Model.Entitys
{
    /// <summary>
    /// Класс-обертка для десериализации json файла в объект City
    /// </summary>
    public class UserLocation
    {
        [JsonPropertyName("location")]
        public City City { get; set; } 
    }
}
