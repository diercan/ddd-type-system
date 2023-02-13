using System.Text.RegularExpressions;
using LanguageExt;
using Sample.FinanceSystem.Domain.Types.Common;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;

namespace Sample.FinanceSystem.Domain.Types.CustomerTypes;
public record VatRegistrationNumber : AbstractStringValueType, IStringValueType<VatRegistrationNumber>
{
    private VatRegistrationNumber(string value) : base(value) { }

    public static Either<ValidationError, VatRegistrationNumber> Parse(string value) =>
        IStringValueType<VatRegistrationNumber>.Parse(
            value => value.Length <= 8 && vatRegistrationNumberFormat.IsMatch(value),
            (value) => new VatRegistrationNumber(value),
            value);

    private static readonly Regex vatRegistrationNumberFormat = new("^[A-Za-z]{2}[0-9]{6}$");
}
