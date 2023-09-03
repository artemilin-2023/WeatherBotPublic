
using System.Text.Json.Serialization;

namespace WeatherBot.Model.Entitys
{

    public class City : BaseEntity
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("lon")]
        public double Longitude { get; set; }

        [JsonPropertyName("lat")]
        public double Latitude { get; set; }

        [JsonPropertyName("region")]
        public string State { get; set; }
    }
}
