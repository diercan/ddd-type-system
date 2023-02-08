using System.Text.RegularExpressions;
using LanguageExt;
using Sample.FinanceSystem.Domain.Types.Common;

namespace Sample.FinanceSystem.Domain.Types.Address;

public record ZipCode : AbstactStringValueType, IStringValueType<ZipCode>
{
    private ZipCode(string value) : base(value) { }

    public static Either<ErrorMessage, ZipCode> Parse(string value)
        => IStringValueType<ZipCode>.Parse(zipCodeFormat, (value) => new ZipCode(value), value);

    private static readonly Regex zipCodeFormat = new("^\\d{9}$");
}
