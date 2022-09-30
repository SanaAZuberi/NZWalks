using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        // Synchronous call
        // IEnumerable<Region> GetAll();
        Task<IEnumerable<Region>> GetAllAsync();

        Task<Region> GetAsync(Guid id);

        Task<Region> AddAsync(Region region);

        Task<Region> UpdateAsync(Guid id, Region region);

        // we can use the return type bool but we are returning object in case clients wants to use it
        Task<Region> DeleteAsync(Guid id);
    }
}
