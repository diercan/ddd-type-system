using LanguageExt;
using Sample.FinanceSystem.Domain.Types.Common;

namespace Sample.FinanceSystem.Domain.Types.CustomerTypes;
public record Code : AbstractStringValueType, IStringValueType<Code>
{
    private Code(string value) : base(value) { }

    public static Either<ErrorMessage, Code> Parse(string value) =>
        IStringValueType<Code>.Parse(
            value => value.Length <= 5,
            (value) => new Code(value),
            value);
}