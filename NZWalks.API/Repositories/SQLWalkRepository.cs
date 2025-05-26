using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext context;

        public SQLWalkRepository(NZWalksDbContext context)
        {
            this.context = context;
        }
        public async Task<Walk?> AddAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await context.Walks.AddAsync(walk);
            await context.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingWalk = await context.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk != null)
            {
                context.Remove(existingWalk);
                await context.SaveChangesAsync();
                return existingWalk;
            }
            return null;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn, string? filterQuery,string? sortBy,bool? isAscending,
            int pageNumber,int pageSize)
        {
            var walks = context.Walks.Include(x => x.Difficulty).Include(x => x.Region).AsQueryable();
            //filter
            if (!(string.IsNullOrEmpty(filterOn) && string.IsNullOrEmpty(filterQuery)))
            {
                if (filterOn.Equals("Name", StringComparison.InvariantCultureIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.ToLower().Contains(filterQuery.ToLower()));
                }
            }
            //sort
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.InvariantCultureIgnoreCase))
                {
                    walks = isAscending == true ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("LengthInKm", StringComparison.InvariantCultureIgnoreCase))
                {
                    walks = isAscending == true ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }
            //pagenation
            int skipPages = (pageNumber - 1) * pageSize;
            walks = walks.Skip(skipPages).Take(pageSize);

            return await walks.ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            var walk = await context.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walk != null)
            {
                return walk;
            }
            return null;
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await context.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk != null)
            {
                existingWalk.Name = walk.Name;
                existingWalk.Description = walk.Description;
                existingWalk.LengthInKm = walk.LengthInKm;
                existingWalk.WalkImageUrl = walk.WalkImageUrl;
                existingWalk.RegionId = walk.RegionId;
                existingWalk.DifficultyId = walk.DifficultyId;
                await context.SaveChangesAsync();
                return existingWalk;
            }
            return null;
        }
    }
}
