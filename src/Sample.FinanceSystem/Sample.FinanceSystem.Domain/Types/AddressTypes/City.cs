using LanguageExt;
using Sample.FinanceSystem.Domain.Types.Common;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;

namespace Sample.FinanceSystem.Domain.Types.AddressTypes;
public record City : AbstractStringValueType, IStringValueType<City>
{
    private City(string value) : base(value) { }

    public static Either<ValidationError, City> Parse(string value) =>
        IStringValueType<City>.Parse(
            value => value.Length > 2 && value.Length <= 50,
            (value) => new City(value),
            value);
}
