using LanguageExt;
using Sample.FinanceSystem.Domain.Types.Common;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;

namespace Sample.FinanceSystem.Domain.Types.VatTypes
{
    public record VatPercentage : AbstractDecimalValueType, IDecimalValueType<VatPercentage>
    {
        protected override string Format => "G";

        private VatPercentage(decimal value) : base(value) { }

        public static Either<ValidationError, VatPercentage> Parse(decimal value)
        {
            if (value < 0)
                return new ValidationError($"{value} is not a valid rate. VAT rate cannot be negative.");
            else if (value > 40)
                return new ValidationError($"{value} is not a valid rate. VAT rate cannot exceed 40%.");

            return new VatPercentage(value);
        }
    }
}
