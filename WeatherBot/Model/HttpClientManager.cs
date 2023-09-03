namespace WeatherBot.Model
{
    public class HttpClientManager
    {
        private readonly IConfiguration configuration;
        private HttpClient httpClient;

        public HttpClientManager(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public HttpClient GetClient()
        {
            if (httpClient != null)
                return httpClient;

            var url = configuration["WeatherBaseUrl"];
            httpClient = new HttpClient() { BaseAddress = new Uri(url) };

            return httpClient;
        }
    }
}
