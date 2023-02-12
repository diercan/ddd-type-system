using System.Text.RegularExpressions;
using LanguageExt;
using Sample.FinanceSystem.Domain.Types.Common;

namespace Sample.FinanceSystem.Domain.Types.PaymentTypes;
public record CardLast4Digits : AbstractStringValueType, IStringValueType<CardLast4Digits>
{
    public static readonly CardLast4Digits Default = new("0000");

    private CardLast4Digits(string value) : base(value) { }

    public static Either<ErrorMessage, CardLast4Digits> Parse(string value)
        => IStringValueType<CardLast4Digits>.Parse(zipCodeFormat, (value) => new CardLast4Digits(value), value);

    private static readonly Regex zipCodeFormat = new("^\\d{4}$");
}
