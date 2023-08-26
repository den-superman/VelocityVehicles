using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using VelocityVehicles.Models;
using VelocityVehicles.Services;

namespace VelocityVehicles.Repositories
{
    public class AutomobileRepository : IAutomobileRepository
    {
        private readonly DBContext _db;
        public AutomobileRepository(DBContext db)
        {
            _db = db;
        }
        public async Task AddNewAsync(Automobile automobile)
        {
            var Automobile = new Automobile()
            {
                AutomobileName = automobile.AutomobileName,
                Brand = automobile.Brand,
                BrandId = automobile.BrandId,
                ImagePath = automobile.ImagePath,
                Price = automobile.Price,
                Providers = automobile.Providers
            };
            await _db.Automobiles.AddAsync(Automobile);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            var entity = await _db.Set<Automobile>().FirstOrDefaultAsync(e => e.Id == id);
            EntityEntry entityEntry = _db.Entry<Automobile>(entity);
            entityEntry.State = EntityState.Deleted;

            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Automobile>> GetAllAsync()
        {
            return await _db.Automobiles.AsQueryable()
                .Include(e => e.Brand)
                .ToListAsync();
        }

        public async Task<Automobile> GetAutomobileAsync(int? id)
        {
            return await _db.Automobiles.AsQueryable()
                .Where(c => c.Id == id)
                .Include(e => e.Brand)
                .Include(o => o.Providers)
                .ThenInclude(a => a.Showroom)
                .SingleAsync();
                
                
        }

        public async Task<IEnumerable<Automobile>> GetByBrandAsync(int? brandid)
        {
            return await _db.Automobiles.AsQueryable()
                .Where(e => e.BrandId == brandid)
                .Include(o => o.Brand)
                .ToListAsync();
        }


        public async Task UpdateAsync(Automobile automobile)
        {
            var auto = await _db.Automobiles.FirstOrDefaultAsync(e => e.Id == automobile.Id);
            if(auto != null)
            {
                auto.ImagePath = automobile.ImagePath;
                auto.Price = automobile.Price;
                auto.AutomobileName = automobile.AutomobileName;
                auto.Providers = automobile.Providers;
                auto.Brand = automobile.Brand;
                auto.BrandId = automobile.BrandId;
                await _db.SaveChangesAsync();
            }
        }

    }
}
