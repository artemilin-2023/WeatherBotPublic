using System.Text.Json.Serialization;

namespace WeatherBot.Model.Entitys
{
    
    public class Weather
    {
        [JsonPropertyName("current")]
        public Current Current { get; set; }

    }

    public class Current
    {
        [JsonPropertyName("condition")]
        public Condition Condition { get; set; }

        [JsonPropertyName("last_updated")]
        public string LastUpdate { get; set; }

        [JsonPropertyName("temp_c")]
        public double TemperatureCelsius { get; set; }

        [JsonPropertyName("feelslike_c")]
        public double FeelsLikeCelsius { get; set; }

        [JsonPropertyName("wind_kph")]
        public double WindSpeed { get; set; }

        [JsonPropertyName("wind_dir")]
        public string WindDirection { get; set; }

        [JsonPropertyName("pressure_mb")]
        public double Pressure { get; set; }

        [JsonPropertyName("precip_mm")]
        public double Precipitation { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }

        [JsonPropertyName("cloud")]
        public int Cloud { get; set; }

        [JsonPropertyName("uv")]
        public double UvIndex { get; set; }

        [JsonPropertyName("is_day")]
        public int IsDay { get; set; }
    }

    public class Condition
    {
        [JsonPropertyName("text")]
        public string Discription { get; set; }
    }
}
