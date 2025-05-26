using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetAllAsync(string? filtrOn,string? filtrQuery,string? sortBy,bool? isAscending=true, int pageNumber=1, int pageSize=1000);
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk?> AddAsync(Walk walk);
        Task<Walk?> UpdateAsync(Guid id, Walk walk);
        Task<Walk?> DeleteAsync(Guid id);
    }
}
