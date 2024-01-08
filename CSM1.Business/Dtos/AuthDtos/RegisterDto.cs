using CSM1.Business.Extensions;
using FluentValidation;

namespace CSM1.Business.Dtos.AuthDtos;

public class RegisterDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime BirthDay { get; set; }
}

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Name).
            NotEmpty().
            CustomLength(2, 32);
        RuleFor(x => x.Surname).
            NotEmpty().
            CustomLength(2, 32);
        RuleFor(x => x.Username).
            NotNullNotEmpty().
            CustomLength(2, 32);
        RuleFor(x => x.Email).
            NotNullNotEmpty().
            EmailAddress();
        RuleFor(x => x.Password).
            NotNullNotEmpty().
            MinimumLength(4).
            Must(x => x.Any(Char.IsUpper)).
            Must(x => x.Any(Char.IsLower)).
            Must(x => x.Any(Char.IsDigit));
    }
}
