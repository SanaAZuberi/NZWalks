using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        // Synchronous call
        // IEnumerable<Region> GetAll();
        Task<IEnumerable<Region>> GetAllAsync();
    }
}
