using VelocityVehicles.Models;

namespace VelocityVehicles.Services
{
    public interface IShowroomRepository
    {
        Task<IEnumerable<Showroom>> GetAllAsync();
        Task AddNewAsync(Showroom showroom);
        Task UpdateAsync(Showroom showroom);
        Task DeleteAsync(int? id);
        Task<Showroom> GetShowroomAsync(int? id);

    }

}
