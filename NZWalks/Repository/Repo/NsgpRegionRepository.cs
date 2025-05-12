using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Dto;
using NZWalks.Dto.RequestDto;
using NZWalks.Models;
using NZWalks.Repository.Interface;

namespace NZWalks.Repository.Repo;

public class NsgpRegionRepository : IRegionRepository
{
    private readonly NZWalksDbContext _dbContext;

    public NsgpRegionRepository(NZWalksDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task< List<Region>> GetAllRegionAsync(string? filterOn, string? filterQuery, string? sortBy, bool isAssigned, int pageSize, int pageNumber)
    {
        //var regionDomain = await _dbContext.Regions.ToListAsync();
        var regionDomain =  _dbContext.Regions.AsQueryable();

        if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
        {
            if (filterQuery.Equals("Name",StringComparison.OrdinalIgnoreCase))
            {
                regionDomain = regionDomain.Where(x => x.Name.Contains(filterQuery));
            }
        }

        if(string.IsNullOrWhiteSpace(sortBy) == false)
        {
            if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                regionDomain = isAssigned ? regionDomain.OrderBy(x => x.Name) : regionDomain.OrderByDescending(x => x.Name);
            }
        }
       
        var skipResult = (pageNumber - 1) * pageSize;


        return await regionDomain.Skip(skipResult).Take(pageSize).ToListAsync();
    }

    public async Task<Region> GetRegionById(Guid id)
    {
        var regionDomain = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        return regionDomain;
    }

    public async Task CreateRegion(Region region)
    {
        await _dbContext.Regions.AddAsync(region);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateRegion(Region region)
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteRegion(Region region)
    {
        _dbContext.Regions.Remove(region);
        await _dbContext.SaveChangesAsync();
    }

}
