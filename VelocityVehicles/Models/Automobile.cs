using NuGet.Common;
using System.ComponentModel.DataAnnotations;

namespace VelocityVehicles.Models
{
    public class Automobile
    {
        [Key]
        public int Id { get; set; }
        public string? AutomobileName { get; set; }
        public string ImagePath { get; set; }
        public decimal Price { get; set; }
        public Brand? Brand { get; set; }
        public int BrandId { get; set; }
        public ICollection<Provider> Providers { get; set; }
        
    }

}
