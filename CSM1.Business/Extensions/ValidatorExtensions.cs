using FluentValidation;

namespace CSM1.Business.Extensions;

public static class ValidatorExtensions
{
    public static IRuleBuilderOptions<T, string> CustomLength<T>(this IRuleBuilder<T, string> ruleBuilder, int minimumLength, int maximumLength)
        => ruleBuilder.MinimumLength(minimumLength).MaximumLength(maximumLength);
    public static IRuleBuilderOptions<T, string> NotNullNotEmpty<T>(this IRuleBuilder<T, string> ruleBuilder)
        => ruleBuilder.NotNull().NotEmpty();
}
