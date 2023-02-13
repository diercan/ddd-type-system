using LanguageExt;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;

namespace Sample.FinanceSystem.Domain.Types.Common
{
    public abstract record AbstractDecimalValueType
    {
        public decimal Value { get; }
        protected abstract string Format { get; }

        protected AbstractDecimalValueType(decimal value)
        {
            Value = value;
        }

        public override string ToString() => Value.ToString(Format);
    }

    internal interface IDecimalValueType<T> where T : AbstractDecimalValueType
    {
        public static abstract Either<ValidationError, T> Parse(decimal value);
    }
}
