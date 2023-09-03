using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WeatherBot.ErrorManagement;
using WeatherBot.Model;
using WeatherBot.Model.Command;
using WeatherBot.Model.Services;
using WeatherBot.Model.Services.Interfaces;

namespace WeatherBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            var curDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var databaseName = builder.Configuration.GetConnectionString("Db");
            var connectionString = Path.Combine(curDirectory, databaseName);
            builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlite("Data Source=" + connectionString), ServiceLifetime.Singleton);

            builder.Services.AddSingleton<TelegramBotClientManager>();
            builder.Services.AddSingleton<HttpClientManager>();
            builder.Services.AddScoped<CommandExecutor>();

            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<ICommand, StartCommand>();
            builder.Services.AddTransient<IGeoService, GeoService>();
            builder.Services.AddTransient<IWeatherService, WeatherService>();

            var app = builder.Build();

            app.UseMiddleware<ExceptionHandling>();
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.Run();
        }
    }
}