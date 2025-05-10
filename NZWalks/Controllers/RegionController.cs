using AutoMapper;
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

    public RegionController(IRegionRepository regionRepository, IMapper mapper)
    {
        _regionRepository = regionRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task <IActionResult> GetAllRegion()
    {
        var regionsDomain = await _regionRepository.GetAllRegionAsync();

        var regions = _mapper.Map<List<RegionDto>>(regionsDomain);

        return Ok(regions);
    }

    [HttpGet]
    [Route("{id:Guid}")]
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
    public async Task<IActionResult> CreateRegion([FromBody] CreateRegionRequestDto requestDto)
    {
       
        var regionDomain = _mapper.Map<Region>(requestDto);

        await _regionRepository.CreateRegion(regionDomain);

        var regionDto = _mapper.Map<RegionDto>(regionDomain);


        return CreatedAtAction(nameof(GetRegionById),new {Id = regionDto.Id}, regionDto);
    }

    [HttpPut]
    [Route("{Id:Guid}")]
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
