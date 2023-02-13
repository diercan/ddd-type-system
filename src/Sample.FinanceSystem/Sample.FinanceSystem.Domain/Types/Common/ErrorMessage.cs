namespace Sample.FinanceSystem.Domain.Types.Common
{
    [AsChoice]
    public static partial class ErrorMessage
    {
        public interface IErrorMessage { }

        public record ValidationError : IErrorMessage
        {
            public string? FieldPath { get; private init; }

            public string Message { get; private init; }

            public ValidationError(string message, string? fieldPath)
            {
                Message = message;
                FieldPath = fieldPath;
            }

            public ValidationError(string message) : this(message, null) { }

            public override string ToString() => Message;
        }

        public record UnexpectedErrorMessage : IErrorMessage
        {
            public string Message { get; private init; }

            public UnexpectedErrorMessage(string message)
            {
                Message = message;
            }

            public UnexpectedErrorMessage(Exception exception) : this(exception.ToString()) { }
        }
    }
}
