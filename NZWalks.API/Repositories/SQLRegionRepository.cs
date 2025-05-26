using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext context;

        public SQLRegionRepository(NZWalksDbContext context)
        {
            this.context = context;
        }
        public async Task<Region?> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await context.AddAsync(region);
            await context.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await context.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion != null)
            {
                context.Remove(existingRegion);
                await context.SaveChangesAsync();
                return existingRegion;
            }
            return null;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await context.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            var region = await context.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region != null)
            {
                return region;
            }
            return null;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await context.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion != null)
            {
                existingRegion.Code = region.Code;
                existingRegion.Name = region.Name;
                existingRegion.RegionImageUrl = region.RegionImageUrl;
                await context.SaveChangesAsync();
                return existingRegion;
            }
            return null;
        }
    }
}
