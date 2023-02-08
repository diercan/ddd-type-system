using System.Text.RegularExpressions;
using LanguageExt;

namespace Sample.FinanceSystem.Domain.Types.Common
{
    public abstract record AbsttactStringValueType
    {
        public string Value { get; }
        protected AbsttactStringValueType(string value)
        {
            Value = value;
        }

        public override string ToString() => Value;
    }

    internal interface IStringValueType<T> where T : AbsttactStringValueType
    {
        public static abstract Either<ErrorMessage, T> Parse(string value);

        protected static Either<ErrorMessage, T> Parse(Regex regex, Func<string, T> createFunc, string value) =>
            Parse(regex.IsMatch, createFunc, value);


        protected static Either<ErrorMessage, T> Parse(Func<string, bool> isValid, Func<string, T> createFunc, string value) =>
            isValid(value)
            ? createFunc(value)
            : new ErrorMessage($"Value {value} is an invalid {typeof(T).Name}");
    }
}
