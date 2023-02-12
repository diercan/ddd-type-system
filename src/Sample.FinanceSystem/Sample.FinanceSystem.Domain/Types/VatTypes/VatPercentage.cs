using LanguageExt;
using Sample.FinanceSystem.Domain.Types.Common;

namespace Sample.FinanceSystem.Domain.Types.VatTypes;
public record VatPercentage : AbstractDecimalValueType, IDecimalValueType<VatPercentage>
{
    protected override string Format => "G";

    private VatPercentage(decimal value) : base(value) { }

    public static Either<ErrorMessage, VatPercentage> Parse(decimal value)
    {
        if (value < 0)
            return new ErrorMessage($"{value} is not a valid rate. VAT rate cannot be negative.");
        else if (value > 40)
            return new ErrorMessage($"{value} is not a valid rate. VAT rate cannot exceed 40%.");

        return new VatPercentage(value);
    }
}
