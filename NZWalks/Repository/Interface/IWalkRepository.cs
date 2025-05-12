using NZWalks.Dto.RequestDto;
using NZWalks.Models;

namespace NZWalks.Repository.Interface;

public interface IWalkRepository
{
    Task CreateWalkAsync(Walk walk);
    Task <List<Walk>> GetAllWalksASync(string?filterOn = null, string?filterQuery = null);

    Task<Walk?> GetWalkByIdAsync(Guid Id);
    Task<Walk?> UpdateWalkAsync(Walk walk, UpdateWalkRequestDto updateWalkRequestDto);
    Task<Walk?>  DeleteWalkAsync(Walk walk);
}
