using Microsoft.EntityFrameworkCore;
using WeatherBot.Model.Entitys;

namespace WeatherBot.Model
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}
