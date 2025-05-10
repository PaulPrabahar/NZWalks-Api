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

    public async Task< List<Region>> GetAllRegionAsync()
    {
        var regionDomain = await _dbContext.Regions.ToListAsync();
        return regionDomain;
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
