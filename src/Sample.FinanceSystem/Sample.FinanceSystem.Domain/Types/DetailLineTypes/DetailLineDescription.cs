using LanguageExt;
using Sample.FinanceSystem.Domain.Types.Common;

namespace Sample.FinanceSystem.Domain.Types.InvoiceDetailLineTypes;

public record DetailLineDescription : AbstractStringValueType, IStringValueType<DetailLineDescription>
{
    private DetailLineDescription(string value) : base(value) { }

    public static Either<ErrorMessage, DetailLineDescription> Parse(string value)
        => IStringValueType<DetailLineDescription>.Parse(
            value => value.Length > 10 && value.Length <= 200,
            (value) => new DetailLineDescription(value),
            value);
}
