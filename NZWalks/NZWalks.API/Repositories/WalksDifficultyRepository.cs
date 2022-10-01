using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalksDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDBContext _NZWalksDBContext;

        public WalksDifficultyRepository(NZWalksDBContext nZWalksDBContext)
        {
            _NZWalksDBContext = nZWalksDBContext;
        }

        public async Task<WalkDifficulty> AddWalkDifficultyAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await _NZWalksDBContext.WalkDifficulty.AddAsync(walkDifficulty);
            await _NZWalksDBContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteWalkDifficultyAsync(Guid id)
        {
            var existingWalkDifficulty = await _NZWalksDBContext.WalkDifficulty.FindAsync(id);

            if (existingWalkDifficulty == null)
                return null;

            _NZWalksDBContext.WalkDifficulty.Remove(existingWalkDifficulty);
            await _NZWalksDBContext.SaveChangesAsync();

            return existingWalkDifficulty;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllWalkDifficultiesAsync()
        {
            return await _NZWalksDBContext.WalkDifficulty.ToListAsync();
        }

        public async Task<WalkDifficulty> GetWalkDifficultyAsync(Guid id)
        {
            return await _NZWalksDBContext.WalkDifficulty.FindAsync(id);
        }

        public async Task<WalkDifficulty> UpdateWalkDifficultyAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            var existingWalkDifficulty = await _NZWalksDBContext.WalkDifficulty.FindAsync(id);

            if (existingWalkDifficulty == null)
                return null;

            existingWalkDifficulty.Code = walkDifficulty.Code;
            await _NZWalksDBContext.SaveChangesAsync();

            return existingWalkDifficulty;
        }
    }
}
