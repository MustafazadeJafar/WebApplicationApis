using FluentValidation;

namespace CustomApi.Business.Dtos.AuthDtos;

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
            MinimumLength(2).
            MaximumLength(32);
        RuleFor(x => x.Surname).
            NotEmpty().
            MinimumLength(2).
            MaximumLength(50);
        RuleFor(x => x.Username).
            NotEmpty().
            NotNull().
            MinimumLength(2).
            MaximumLength(25);
    }
}