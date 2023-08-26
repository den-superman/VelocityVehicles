using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using VelocityVehicles.Models;
using VelocityVehicles.Services;

namespace VelocityVehicles.Repositories
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly DBContext _db;
        public ProviderRepository(DBContext db)
        {
            _db = db;
        }
        public async Task AddNewAsync(Provider provider)
        {
            var prov = new Provider()
            {
                Automobile = provider.Automobile,
                AutomobileId = provider.AutomobileId,
                Showroom = provider.Showroom,
                ShowroomId = provider.ShowroomId
            };
            await _db.Providers.AddAsync(prov);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            var entity = await _db.Set<Provider>().FirstOrDefaultAsync(e => e.Id == id);
            EntityEntry entityEntry = _db.Entry<Provider>(entity);
            entityEntry.State = EntityState.Deleted;

            await _db.SaveChangesAsync();
            
        }

        public async Task<IEnumerable<Provider>> GetAllAsync()
        {
            return await _db.Providers.AsQueryable()
                .Include(e => e.Automobile)
                .Include(z => z.Showroom)
                .ToListAsync();
        }

        public async Task<Provider> GetProviderAsync(int? id)
        {
            return await _db.Providers.AsQueryable()
                .Where(e => e.Id == id)
                .Include(e => e.Automobile)
                .Include(z => z.Showroom)
                .SingleAsync();
        }

        public async Task UpdateAsync(Provider provider)
        {
            var pro = await _db.Providers.FirstOrDefaultAsync(e => e.Id == provider.Id);
            if(pro != null)
            {
                pro.Automobile = provider.Automobile;
                pro.AutomobileId = provider.AutomobileId;
                pro.Showroom = provider.Showroom;
                pro.ShowroomId = provider.ShowroomId;
                await _db.SaveChangesAsync();
            }
        }
    }
}
