using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Dto.RequestDto;
using NZWalks.Models;
using NZWalks.Repository.Interface;

namespace NZWalks.Repository.Repo;

public class NsgpWalkRepository : IWalkRepository
{
    private readonly NZWalksDbContext _dbContext;
    private readonly IMapper _mapper;

    public NsgpWalkRepository(NZWalksDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task CreateWalkAsync(Walk walk)
    {
        await _dbContext.AddAsync(walk);
        await _dbContext.SaveChangesAsync();
    }

    public async Task <List<Walk>> GetAllWalksASync()
    {
        var result = await _dbContext.Walks
            .Include("Region")
            .Include("Difficulty")
            .ToListAsync();
        return result;

    }

    public async Task<Walk?> GetWalkByIdAsync(Guid Id)
    {
        var result = await _dbContext.Walks
            .Include("Region")
            .Include("Difficulty")
            .FirstOrDefaultAsync(x => x.Id == Id);
        return result;
    }

    public async Task<Walk?> UpdateWalkAsync(Walk walk, UpdateWalkRequestDto updateWalkRequestDto)
    {
        _mapper.Map(updateWalkRequestDto, walk);
        await _dbContext.SaveChangesAsync();
        return walk;
    }

    public async Task<Walk?> DeleteWalkAsync(Walk walk)
    {
        _dbContext.Remove(walk);
        await _dbContext.SaveChangesAsync();
        return walk;
    }
}
