using Microsoft.AspNetCore.Mvc;
using NZWalks.Dto;
using NZWalks.Dto.RequestDto;
using NZWalks.Models;

namespace NZWalks.Repository.Interface;

public interface IRegionRepository
{
    Task<List<Region>> GetAllRegionAsync();
    Task<Region> GetRegionById(Guid id);
    Task CreateRegion(Region region);
    Task UpdateRegion(Region region);
    Task DeleteRegion (Region region);
}
