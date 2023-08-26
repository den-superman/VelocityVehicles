using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using VelocityVehicles.Models;
using VelocityVehicles.Services;

namespace VelocityVehicles.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly DBContext _db;
        public BrandRepository(DBContext db)
        {
            _db = db;
        }
        public async Task AddNewAsync(Brand brand)
        {
            var NewBrand = new Brand()
            {
                Automobiles = brand.Automobiles,
                BrandDescription = brand.BrandDescription,
                BrandName = brand.BrandName,
                ImagePath = brand.ImagePath
            };
            await _db.Brands.AddAsync(NewBrand);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            var entity = await _db.Set<Brand>().FirstOrDefaultAsync(e => e.Id == id);
            EntityEntry entityEntry = _db.Entry<Brand>(entity);
            entityEntry.State = EntityState.Deleted;

            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            return await _db.Brands.AsQueryable()
                .ToListAsync();
        }

        public async Task<Brand> GetBrandAsync(int id)
        {
            return await _db.Brands.AsQueryable()
                .Where(e => e.Id == id)
                .Include(z => z.Automobiles)
                .SingleAsync();
        }

        public async Task UpdateAsync(Brand brand)
        {
            var Brand = await _db.Brands.FirstOrDefaultAsync(e => e.Id == brand.Id);
            if(Brand != null)
            {
                Brand.Automobiles = brand.Automobiles;
                Brand.BrandName = brand.BrandName;
                Brand.BrandDescription = brand.BrandDescription;
                Brand.ImagePath = brand.ImagePath;
                await _db.SaveChangesAsync();
            }
        }
    }
}
