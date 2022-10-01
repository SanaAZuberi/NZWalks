using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalksRepository : IWalksReposiory
    {
        private readonly NZWalksDBContext _NZWalksDBContext;

        public WalksRepository(NZWalksDBContext nZWalksDBContext)
        {
            _NZWalksDBContext = nZWalksDBContext;
        }

        public async Task<Walk> AddWalkAsync(Walk walk)
        {
            // we need to generate new Id on our own and override in incoming object
            walk.Id = Guid.NewGuid();
            await _NZWalksDBContext.Walks.AddAsync(walk);
            await _NZWalksDBContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> DeleteWalkAsync(Guid id)
        {
            // first we will check if the data exists
            var existingWalk = await _NZWalksDBContext.Walks
                // Updated to include Region detail and WalkDifficulty detail but not verified
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
                //.FindAsync(id);

            if (existingWalk == null)
                return null;

            // delete if found
            _NZWalksDBContext.Walks.Remove(existingWalk);
            await _NZWalksDBContext.SaveChangesAsync();
            return existingWalk;
        }
                
        public async Task<IEnumerable<Walk>> GetAllWalksAsync()
        {
            return await _NZWalksDBContext.Walks
                // include the navigation properties (associations between tables)
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Walk> GetWalkAsync(Guid id)
        {
            var walk = await _NZWalksDBContext.Walks
                // include the navigation properties (associations between tables)
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
            return walk;
        }

        public async Task<Walk> UpdateWalkAsync(Guid id, Walk walk)
        {
            // Updated to include Region detail and WalkDifficulty detail but not verified
            var existingWalk = await _NZWalksDBContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existingWalk == null)
                return null;

            existingWalk.Name = walk.Name;
            existingWalk.Length = walk.Length;
            existingWalk.RegionId = walk.RegionId;
            existingWalk.WalkDifficultyId = walk.WalkDifficultyId; 

            await _NZWalksDBContext.SaveChangesAsync();

            return existingWalk;
        }
    }
}
