using FluentValidation;

namespace CSM1.Business.Dtos.TopicDtos
{
    public class TopicUpdateDto
    {
        public string Name { get; set; }
    }
    public class TopicUpdateDtoValidator : AbstractValidator<TopicUpdateDto>
    {
        public TopicUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(32);
        }
    }
}
