using System.Text.RegularExpressions;
using LanguageExt;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;

namespace Sample.FinanceSystem.Domain.Types.Common
{
    public abstract record AbstractStringValueType
    {
        public string Value { get; }
        protected AbstractStringValueType(string value)
        {
            Value = value;
        }

        public override string ToString() => Value;
    }

    internal interface IStringValueType<T> where T : AbstractStringValueType
    {
        public static abstract Either<ValidationError, T> Parse(string value);

        protected static Either<ValidationError, T> Parse(Regex regex, Func<string, T> createFunc, string value) =>
            Parse(regex.IsMatch, createFunc, value);


        protected static Either<ValidationError, T> Parse(Func<string, bool> isValid, Func<string, T> createFunc, string value) =>
            isValid(value)
            ? createFunc(value)
            : new ValidationError($"Value {value} is an invalid {typeof(T).Name}");
    }
}
