using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalksReposiory
    {
        Task<IEnumerable<Walk>> GetAllWalksAsync();

        Task<Walk> GetWalkAsync(Guid id);

        Task<Walk> AddWalkAsync(Walk walk);

        Task<Walk> UpdateWalkAsync(Guid id, Walk walk);

        Task<Walk> DeleteWalkAsync(Guid id);
    }
}
