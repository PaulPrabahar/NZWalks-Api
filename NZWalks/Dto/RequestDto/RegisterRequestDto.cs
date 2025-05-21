using System.ComponentModel.DataAnnotations;

namespace NZWalks.Dto.RequestDto;

public class RegisterRequestDto
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Password { get; set; }

    [Required]
    public string[] Roles { get; set; }
}
