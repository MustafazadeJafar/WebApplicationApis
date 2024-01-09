using FluentValidation;

namespace CSM1.Business.Extensions;

public static class ValidatorExtensions
{
    public static IRuleBuilderOptions<T, string> CustomLength<T>(this IRuleBuilder<T, string> ruleBuilder, int minimumLength, int maximumLength)
        => ruleBuilder.MinimumLength(minimumLength).MaximumLength(maximumLength);

    public static IRuleBuilderOptions<T, TProperty> NotNullOrEmpty<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        => ruleBuilder.NotNull().NotEmpty();

    public static IRuleBuilderOptions<T, TProperty> CustomRange<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, TProperty Minimum, TProperty Maximum)
    where TProperty : IComparable<TProperty>, IComparable
    {
        return ruleBuilder.GreaterThan(Minimum).WithMessage("Mamont!").
            LessThan(Maximum).WithMessage("dodagindaki sudun quruyanda gelersen");
    }
}
