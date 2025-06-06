﻿using AutoMapper;
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

    public async Task <List<Walk>> GetAllWalksASync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageSize = 1, int pageNumber = 100)
    {
        //var result = await _dbContext.Walks
        //    .Include("Region")
        //    .Include("Difficulty")
        //    .ToListAsync();

        var result = _dbContext.Walks.Include("Region").Include("Difficulty").AsQueryable();

        if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false )
        {
            if (filterOn.Equals("Name",StringComparison.OrdinalIgnoreCase))
            {
                result = result.Where(x => x.Name.Contains(filterQuery));
            }
        }

        if (string.IsNullOrWhiteSpace(sortBy) == false )
        {
            if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                result = isAscending ? result.OrderBy(x => x.Name) : result.OrderByDescending(x => x.Name);
            }
        }

        var skipResult = (pageNumber - 1) * pageSize;

        return await result.Skip(skipResult).Take(pageSize).ToListAsync();

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
