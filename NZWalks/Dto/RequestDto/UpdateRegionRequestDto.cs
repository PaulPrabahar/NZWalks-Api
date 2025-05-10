using System.ComponentModel.DataAnnotations;

namespace NZWalks.Dto.RequestDto;

public class UpdateRegionRequestDto
{
    [MinLength(3, ErrorMessage = "Code must be at least 3 characters long")]
    [MaxLength(3, ErrorMessage = "Code must be less than 3 characters")]
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? RegionImageUrl { get; set; }
}
