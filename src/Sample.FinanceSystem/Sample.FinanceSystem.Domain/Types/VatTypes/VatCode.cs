using LanguageExt;
using Sample.FinanceSystem.Domain.Types.Common;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;

namespace Sample.FinanceSystem.Domain.Types.VatTypes
{
    public record VatCode : AbstractStringValueType, IStringValueType<VatCode>
    {
        public VatCode(string value) : base(value) { }

        public static Either<ValidationError, VatCode> Parse(string value)
            => IStringValueType<VatCode>.Parse(
                value => value.Length > 2 && value.Length < 10,
                value => new VatCode(value),
                value);
    }
}
