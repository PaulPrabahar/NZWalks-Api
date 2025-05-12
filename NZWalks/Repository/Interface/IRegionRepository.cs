using Microsoft.AspNetCore.Mvc;
using NZWalks.Dto;
using NZWalks.Dto.RequestDto;
using NZWalks.Models;

namespace NZWalks.Repository.Interface;

public interface IRegionRepository
{
    Task<List<Region>> GetAllRegionAsync(string? filterOn, string? filterQuery, string? sortBy, bool isAssigned, int pageSize, int pageNumber);
    Task<Region> GetRegionById(Guid id);
    Task CreateRegion(Region region);
    Task UpdateRegion(Region region);
    Task DeleteRegion (Region region);
}
