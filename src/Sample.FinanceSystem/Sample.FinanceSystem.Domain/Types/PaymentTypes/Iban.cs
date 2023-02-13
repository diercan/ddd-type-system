using System.Text.RegularExpressions;
using LanguageExt;
using Sample.FinanceSystem.Domain.Types.Common;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;

namespace Sample.FinanceSystem.Domain.Types.PaymentTypes;
public record Iban : AbstractStringValueType, IStringValueType<Iban>
{
    public static readonly Iban Default = new("XX00 0000 0000 0000 0000 000 0");

    private Iban(string value) : base(value)
    {
    }

    public static Either<ValidationError, Iban> Parse(string value) =>
        IStringValueType<Iban>.Parse(IbanFormat, (value) => new(value), value);

    private static readonly Regex IbanFormat = new("\\b[A-Z]{2}[0-9]{2}(?:[ ]?[0-9]{4}){4}(?!(?:[ ]?[0-9]){3})(?:[ ]?[0-9]{1,2})?\\b");
}
