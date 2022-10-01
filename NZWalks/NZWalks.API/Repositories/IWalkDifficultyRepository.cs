﻿using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetAllWalkDifficultiesAsync();

        Task<WalkDifficulty> GetWalkDifficultyAsync(Guid id);

        Task<WalkDifficulty> AddWalkDifficultyAsync(WalkDifficulty walkDifficulty);

        Task<WalkDifficulty> UpdateWalkDifficultyAsync(Guid id, WalkDifficulty walkDifficulty);

        Task<WalkDifficulty> DeleteWalkDifficultyAsync(Guid id);
    }
}
