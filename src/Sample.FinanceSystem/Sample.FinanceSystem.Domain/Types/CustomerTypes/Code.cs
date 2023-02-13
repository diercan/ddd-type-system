using LanguageExt;
using Sample.FinanceSystem.Domain.Types.Common;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;

namespace Sample.FinanceSystem.Domain.Types.CustomerTypes;
public record Code : AbstractStringValueType, IStringValueType<Code>
{
    private Code(string value) : base(value) { }

    public static Either<ValidationError, Code> Parse(string value) =>
        IStringValueType<Code>.Parse(
            value => value.Length <= 5,
            (value) => new Code(value),
            value);
}