using System.ComponentModel.DataAnnotations;

namespace VelocityVehicles.Models
{
    public class Showroom
    {
        [Key]
        public int Id { get; set; }
        public string? ShowroomName { get; set; }
        public string? ShowroomDescription { get; set; }
        public ICollection<Provider> Providers { get; set; }
    }

}
