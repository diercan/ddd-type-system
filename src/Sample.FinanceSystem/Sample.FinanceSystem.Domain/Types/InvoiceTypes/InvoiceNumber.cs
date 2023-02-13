using LanguageExt;
using Sample.FinanceSystem.Domain.Types.Common;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;

namespace Sample.FinanceSystem.Domain.Types.InvoiceTypes;
public record InvoiceNumber : AbstractStringValueType, IStringValueType<InvoiceNumber>
{
    private InvoiceNumber(string value) : base(value) { }

    //TODO define invoice number format
    public static Either<ValidationError, InvoiceNumber> Parse(string value) =>
        IStringValueType<InvoiceNumber>.Parse(
            value => value.Length > 1 && value.Length <= 10,
            (value) => new InvoiceNumber(value),
            value);
}
