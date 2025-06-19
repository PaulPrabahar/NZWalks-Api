using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.CustomValidation;
using NZWalks.Data;
using NZWalks.Dto;
using NZWalks.Dto.RequestDto;
using NZWalks.Models;
using NZWalks.Repository.Interface;

namespace NZWalks.Controllers;

[Route("api/[controller]")]
[ApiController]

public class RegionController : ControllerBase
{
    private readonly IRegionRepository _regionRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<RegionController> _logger;

    public RegionController(IRegionRepository regionRepository, IMapper mapper, ILogger<RegionController> logger)
    {
        _regionRepository = regionRepository;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    //[Authorize(Roles = "Reader")]
    public async Task <IActionResult> GetAllRegion([FromQuery] string?filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool isAssigned, [FromQuery] int pageSize, [FromQuery] int pageNumber)
    {
        try
        {
            var regionsDomain = await _regionRepository.GetAllRegionAsync(filterOn, filterQuery, sortBy, isAssigned, pageSize, pageNumber);

            var regions = _mapper.Map<List<RegionDto>>(regionsDomain);

            return Ok(regions);
        }
        catch (Exception ex) 
        { 
            _logger.LogError(ex,ex.Message);
            throw;
        }
    }

    [HttpGet]
    [Route("{id:Guid}")]
    [Authorize(Roles = "Reader")]
    public async Task <IActionResult> GetRegionById(Guid id)
    {
        var regionsDomain = await _regionRepository.GetRegionById(id);

        if (regionsDomain == null)
        {
            return NotFound();
        }

        var regions = _mapper.Map<RegionDto>(regionsDomain);

        return Ok(regions);
    }

    [HttpPost]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> CreateRegion([FromBody] CreateRegionRequestDto requestDto)
    {
       
        var regionDomain = _mapper.Map<Region>(requestDto);

        await _regionRepository.CreateRegion(regionDomain);

        var regionDto = _mapper.Map<RegionDto>(regionDomain);


        return CreatedAtAction(nameof(GetRegionById),new {Id = regionDto.Id}, regionDto);
    }

    [HttpPut]
    [Route("{Id:Guid}")]
    [Authorize(Roles = "Writer")]
    [ValidateAction]
    public async Task<IActionResult> UpdateRegion([FromRoute] Guid Id, [FromBody] UpdateRegionRequestDto requestDto)
    {
            var regionDomain = await _regionRepository.GetRegionById(Id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            //regionDomain.Code = requestDto.Code ?? regionDomain.Code;
            //regionDomain.Name = requestDto.Name ?? regionDomain.Name;
            //regionDomain.RegionImageUrl = requestDto.RegionImageUrl ?? regionDomain.RegionImageUrl;

            regionDomain = _mapper.Map<Region>(requestDto);

            await _regionRepository.UpdateRegion(regionDomain);

            var regionDto = _mapper.Map<RegionDto>(regionDomain);

            return Ok(regionDto);
        
    }

    [HttpDelete]
    [Route("{Id:Guid}")]
    [Authorize(Roles = "Reader, Writer")]
    public async Task<IActionResult> DeleteRegion([FromRoute] Guid Id)
    {
        var regionDomain = await _regionRepository.GetRegionById(Id);

        if (regionDomain == null)
        {
            return NotFound();
        }

       await _regionRepository.DeleteRegion(regionDomain);

        var regionDto = _mapper.Map<RegionDto>(regionDomain);

        return Ok(regionDto);
    }
}
