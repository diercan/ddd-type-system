using System.Text;

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

        public record AggregatedValidationError : IErrorMessage
        {
            public IReadOnlyCollection<ValidationError> Errors { get; private init; }

            public AggregatedValidationError(IEnumerable<ValidationError> validationErrors)
            {
                Errors = validationErrors.ToList().AsReadOnly();
            }

            public override string ToString() =>
                Errors.Aggregate(
                 new StringBuilder(),
                    (builder, error) => builder.Length is 0
                        ? builder.Append(error.ToString())
                        : builder.AppendLine(error.ToString())
                ).ToString();
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
