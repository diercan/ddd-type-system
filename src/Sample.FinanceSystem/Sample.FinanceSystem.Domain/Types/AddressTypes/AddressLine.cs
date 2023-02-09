using LanguageExt;
using Sample.FinanceSystem.Domain.Types.Common;

namespace Sample.FinanceSystem.Domain.Types.AddressTypes;
public record AddressLine : AbstractStringValueType, IStringValueType<AddressLine>
{
    public AddressLine(string value) : base(value) { }

    public static Either<ErrorMessage, AddressLine> Parse(string value) =>
        IStringValueType<AddressLine>.Parse(
            value => value.Length <= 200,
            (value) => new AddressLine(value),
            value);
}
