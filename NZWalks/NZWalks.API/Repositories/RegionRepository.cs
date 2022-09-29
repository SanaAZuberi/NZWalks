using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDBContext _NZWalksDBContext;

        public RegionRepository(NZWalksDBContext nZWalksDBContext)
        {
            _NZWalksDBContext = nZWalksDBContext;
        }

        // Synchronous implementation
        // public IEnumerable<Region> GetAll()
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await _NZWalksDBContext.Regions.ToListAsync();
        }
    }
}
