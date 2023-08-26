using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VelocityVehicles.Models;

namespace VelocityVehicles.Repositories
{
    public class DBContext: IdentityDbContext<User>
    {
        public DBContext(DbContextOptions<DBContext> options)
         : base(options) { }

        public DbSet<Automobile> Automobiles { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Showroom> Showrooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
