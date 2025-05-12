using Microsoft.AspNetCore.Mvc;
using NZWalks.Dto.RequestDto;
using NZWalks.Models;

namespace NZWalks.Repository.Interface;

public interface IWalkRepository
{
    Task CreateWalkAsync(Walk walk);
    Task <List<Walk>> GetAllWalksASync(string?filterOn = null, string?filterQuery = null, string?sortBy = null, bool isAscending = true, int pageSize = 1,  int pageNumber = 100);

    Task<Walk?> GetWalkByIdAsync(Guid Id);
    Task<Walk?> UpdateWalkAsync(Walk walk, UpdateWalkRequestDto updateWalkRequestDto);
    Task<Walk?>  DeleteWalkAsync(Walk walk);
}
