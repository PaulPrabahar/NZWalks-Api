using AutoMapper;
using NZWalks.Dto;
using NZWalks.Dto.RequestDto;
using NZWalks.Models;

namespace NZWalks.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Region, RegionDto>().ReverseMap();
        CreateMap<Region, CreateRegionRequestDto>().ReverseMap();
        CreateMap<UpdateRegionRequestDto, Region>()
            .ForAllMembers(opt => opt.Condition((src,dest,srcMember) => srcMember != null));

        CreateMap<Walk, CreateWalkRequestDto>().ReverseMap();
        CreateMap<Walk, WalkDto>().ReverseMap();
        CreateMap<Difficulty, DifficultyDto>().ReverseMap();

        CreateMap<UpdateWalkRequestDto, Walk>()
            .ForAllMembers(opt => opt.Condition((src,dest,srcMember)=> srcMember != null));
    }
}
