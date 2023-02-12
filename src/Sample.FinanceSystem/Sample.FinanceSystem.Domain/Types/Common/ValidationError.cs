namespace Sample.FinanceSystem.Domain.Types.Common;
public record ValidationError(string ErrorMessage, string FieldPath);
