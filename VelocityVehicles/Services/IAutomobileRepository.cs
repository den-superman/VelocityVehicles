using VelocityVehicles.Models;

namespace VelocityVehicles.Services
{
    public interface IAutomobileRepository
    {
        Task AddNewAsync(Automobile automobile);
        Task UpdateAsync(Automobile automobile);
        Task DeleteAsync(int? id);
        Task<Automobile> GetAutomobileAsync(int? id);
        Task<IEnumerable<Automobile>> GetAllAsync();
        Task<IEnumerable<Automobile>> GetByBrandAsync(int? brandid);

    }

}
