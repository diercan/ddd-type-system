using LanguageExt;
using Sample.FinanceSystem.Domain.Types.Common;

namespace Sample.FinanceSystem.Domain.Types.AddressTypes;
public record City : AbstractStringValueType, IStringValueType<City>
{
    private City(string value) : base(value) { }

    public static Either<ErrorMessage, City> Parse(string value) =>
        IStringValueType<City>.Parse(
            value => value.Length <= 50,
            (value) => new City(value),
            value);
}
