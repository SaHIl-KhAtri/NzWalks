using Microsoft.EntityFrameworkCore;
using NZWalksApi.Data;
using NZWalksApi.Model.Domain;

namespace NZWalksApi.Repository
{
    public class ImplementRepository : IRepository
    {
        private readonly ApplicationDbContext _dbContext; 
        
        public ImplementRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }


        public async Task<List<Region>> GetAllAsync()
        {
            return await _dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Regions.FirstOrDefaultAsync(x =>x.Id == id);
        }

        public async Task<Region> CreateRegionAsync(Region region)
        {
            await _dbContext.AddAsync(region);
            _dbContext.SaveChanges();
            return region;
        }

        public async Task<Region?> UpdateRegionAsync(Guid id, Region region)
        {
            var existRegion = await _dbContext.Regions.FirstOrDefaultAsync(x=>x.Id == id); 
            if(existRegion == null)
            {
                return null;
            }
            existRegion.Code = region.Code;
            existRegion.Name = region.Name;
            existRegion.RegionImageUrl = region.RegionImageUrl;
            _dbContext.SaveChangesAsync();
            return existRegion;
        }

        public async Task<Region?> DeleteRegionAsync(Guid id)
        {
            var existRegion = _dbContext.Regions.FirstOrDefault(x=>x.Id == id);
            if(existRegion == null)
            {
                return null;
            }
            _dbContext.Remove(existRegion);
            await _dbContext.SaveChangesAsync();
            return existRegion;
        }
    }
}
