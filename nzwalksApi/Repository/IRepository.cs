using NZWalksApi.Model.Domain;

namespace NZWalksApi.Repository
{
    public interface IRepository
    {
        Task<List<Region>> GetAllAsync();

        Task<Region?> GetByIdAsync(Guid id);
        Task<Region> CreateRegionAsync(Region region);

        Task<Region?> UpdateRegionAsync(Guid id, Region region);
        Task<Region?> DeleteRegionAsync(Guid id);

    }
}
