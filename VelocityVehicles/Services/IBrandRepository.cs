using VelocityVehicles.Models;

namespace VelocityVehicles.Services
{
    public interface IBrandRepository {
        Task AddNewAsync(Brand brand);
        Task UpdateAsync(Brand brand);
        Task DeleteAsync(int? id);
        Task<Brand> GetBrandAsync(int id);
        Task<IEnumerable<Brand>> GetAllAsync();


    }

}
