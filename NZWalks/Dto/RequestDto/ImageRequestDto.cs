namespace NZWalks.Dto.RequestDto;

public class ImageRequestDto
{
    public IFormFile File { get; set; }
    public string FileName { get; set; }
    public string? FileDescription { get; set; }
}
