using LanguageExt;
using Sample.FinanceSystem.Domain.Types.Common;

namespace Sample.FinanceSystem.Domain.Types.CustomerTypes;
public record Name : AbstractStringValueType, IStringValueType<Name>
{
    private Name(string value) : base(value) { }

    public static Either<ErrorMessage, Name> Parse(string value) =>
        IStringValueType<Name>.Parse(
            value => value.Length <= 100,
            (value) => new Name(value),
            value);
}
