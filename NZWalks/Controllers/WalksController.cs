using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Dto;
using NZWalks.Dto.RequestDto;
using NZWalks.Models;
using NZWalks.Repository.Interface;

namespace NZWalks.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WalksController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IWalkRepository _repository;

    public WalksController(IMapper mapper, IWalkRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateWalk([FromBody] CreateWalkRequestDto requestDto)
    {
        var walkDomain = _mapper.Map<Walk>(requestDto);

        await _repository.CreateWalkAsync(walkDomain);

        var WalkDto = _mapper.Map<WalkDto>(walkDomain);

        return Ok(WalkDto);   
    }

    [HttpGet]
    public async Task<IActionResult> GetAllWalks([FromQuery] string?filterOn, [FromQuery] string?filterQuery, [FromQuery] string?sortBy, [FromQuery] bool isAscending, [FromQuery] int pageSize, [FromQuery] int pageNumber)
    {
        var walkDomain = await _repository.GetAllWalksASync(filterOn, filterQuery, sortBy, isAscending, pageSize, pageNumber);

        var walkDto = _mapper.Map<List<WalkDto>>(walkDomain);

        return Ok(walkDto);
    }

    [HttpGet]
    [Route("{Id:Guid}")]

    public async Task<IActionResult> GetWalksById([FromRoute] Guid Id)
    {
        var walkDomain = await _repository.GetWalkByIdAsync( Id);

        if (walkDomain == null)
        {
            return NotFound();
        }

        var walkDto = _mapper.Map<WalkDto>(walkDomain);

        return Ok(walkDto);
    }

    [HttpPut]
    [Route("{Id:Guid}")]

    public async Task<IActionResult> UpdateWalks([FromRoute] Guid Id, UpdateWalkRequestDto updateWalkRequestDto)
    {
        var walkDomain = await _repository.GetWalkByIdAsync(Id);
        if (walkDomain == null)
        {
            return NotFound();
        }

        var walkUpdated = await _repository.UpdateWalkAsync( walkDomain, updateWalkRequestDto);

        var walkDto = _mapper.Map<WalkDto>(walkUpdated);

        return Ok(walkDto);
    }

    [HttpDelete]
    [Route("{Id:Guid}")]

    public async Task<IActionResult> DeleteWalks([FromRoute] Guid Id)
    {
        var walkDomain = await _repository.GetWalkByIdAsync(Id);
        if ( walkDomain == null)
        {
            return NotFound();
        }

        var WalkUpdate = await _repository.DeleteWalkAsync( walkDomain);

        var walkDto = _mapper.Map<WalkDto>(walkDomain);

        return Ok(walkDto);
    }
}
