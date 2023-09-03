using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherBot.Model.Entitys
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
        
        public readonly string CreatedAt;

        public BaseEntity()
        {
            CreatedAt = DateTime.UtcNow.ToString();
        }
    }
}
