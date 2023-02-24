namespace Sample.FinanceSystem.Domain.Types.VatTypes;

public record VatContext(
    int Id,
    string Code,
    decimal Percentage,
    DateOnly StarDate);
