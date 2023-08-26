using VelocityVehicles.Models;

namespace VelocityVehicles.Services
{
    public interface IProviderRepository
    {
        Task<IEnumerable<Provider>> GetAllAsync();
        Task<Provider> GetProviderAsync(int? id);
        Task AddNewAsync(Provider provider);
        Task UpdateAsync(Provider provider);
        Task DeleteAsync(int? id);
    }

}
