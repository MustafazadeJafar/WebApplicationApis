using CSM1.Business.Extensions;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace CSM1.Business.Dtos.AuthDtos;

public class LoginDto
{
    public string UsernameOrEmail { get; set; }
    public string Password { get; set; }
}
public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.UsernameOrEmail).
            NotNullOrEmpty().
            CustomLength(3, 64);
        RuleFor(x => x.Password).
            NotNullOrEmpty().
            CustomLength(3, 64);
    }
}