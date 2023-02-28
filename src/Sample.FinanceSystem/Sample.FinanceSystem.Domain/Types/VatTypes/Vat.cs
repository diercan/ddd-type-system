namespace Sample.FinanceSystem.Domain.Types.VatTypes;

public record Vat(
    VatCode Code,
    VatPercentage Percentage);

public record UnvalidatedVat(
    string Code,
    decimal? Percentage = null);
