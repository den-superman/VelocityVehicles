using System.ComponentModel.DataAnnotations;

namespace VelocityVehicles.Models
{
    public class Provider
    {
        [Key]
        public int Id { get; set; }
        public int AutomobileId { get; set; }
        public int ShowroomId { get; set; }
        public Automobile? Automobile { get; set; }
        public Showroom? Showroom { get; set; }

    }

}
