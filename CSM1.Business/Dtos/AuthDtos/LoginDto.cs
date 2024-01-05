using System.ComponentModel.DataAnnotations;

namespace CSM1.Business.Dtos.AuthDtos;

public class LoginDto
{
    public string UsernameOrEmail { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public bool IsRemember { get; set; }
}
