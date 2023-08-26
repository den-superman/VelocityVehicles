using System.ComponentModel.DataAnnotations;

namespace VelocityVehicles.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        public string? BrandName { get; set; }
        public string? BrandDescription { get; set; }
        public string ImagePath { get; set; }
        public ICollection<Automobile> Automobiles { get; set; }
    }

}
