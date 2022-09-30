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

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await _NZWalksDBContext.AddAsync(region);
            await _NZWalksDBContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            // first we will check if the data exists
            var region = await _NZWalksDBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (region == null)
                return null;

            // delete
            _NZWalksDBContext.Regions.Remove(region);
            await _NZWalksDBContext.SaveChangesAsync();
            return region;
        }

        // Synchronous implementation
        // public IEnumerable<Region> GetAll()
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await _NZWalksDBContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
            var region = await _NZWalksDBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            return region;
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await _NZWalksDBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null)
                return null;

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Lattitude = region.Lattitude;
            existingRegion.Longitude = region.Longitude;
            existingRegion.Population = region.Population;

            await _NZWalksDBContext.SaveChangesAsync();

            return existingRegion;
        }
    }
}
