using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using VelocityVehicles.Models;
using VelocityVehicles.Services;

namespace VelocityVehicles.Repositories
{
    public class ShowroomRepository : IShowroomRepository
    {
        private readonly DBContext _db;
        public ShowroomRepository(DBContext db)
        {
            _db = db;
        }
        public async Task AddNewAsync(Showroom showroom)
        {
            var NewShowroom = new Showroom()
            {
                Providers = showroom.Providers,
                ShowroomDescription = showroom.ShowroomDescription,
                ShowroomName = showroom.ShowroomName
            };
            await _db.Showrooms.AddAsync(NewShowroom);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            var entity = await _db.Set<Showroom>().FirstOrDefaultAsync(e => e.Id == id);
            EntityEntry entityEntry = _db.Entry<Showroom>(entity);
            entityEntry.State = EntityState.Deleted;

            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Showroom>> GetAllAsync()
        {
            return await _db.Showrooms.AsQueryable()
                .ToListAsync();
        }

        public async Task<Showroom> GetShowroomAsync(int? id)
        {
            return await _db.Showrooms.AsQueryable()
                .Where(c => c.Id == id)
                .Include(o => o.Providers)
                .ThenInclude(r => r.Automobile)
                .ThenInclude(c => c.Brand)
                .SingleAsync();
        }

        public async Task UpdateAsync(Showroom showroom)
        {
            var show = await _db.Showrooms.FirstOrDefaultAsync(e => e.Id == showroom.Id);
            if(show != null)
            {
                show.ShowroomDescription = showroom.ShowroomDescription;
                show.Providers = showroom.Providers;
                show.ShowroomName = showroom.ShowroomName;
                await _db.SaveChangesAsync();
            }
        }
    }
}
