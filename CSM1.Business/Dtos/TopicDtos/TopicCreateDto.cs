using CSM1.Business.Extensions;
using FluentValidation;

namespace CSM1.Business.Dtos.TopicDtos;

public class TopicCreateDto
{
    public string Name { get; set; }
}
public class TopicCreateDtoValidator : AbstractValidator<TopicCreateDto>
{
    public TopicCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .CustomLength(2,32);
    }
}
