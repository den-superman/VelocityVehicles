using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VelocityVehicles.Models
{
    public class User: IdentityUser
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }
    }
}
