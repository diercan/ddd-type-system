﻿namespace Sample.FinanceSystem.Domain.Types.VatTypes;

public record Vat(
    VatCode Code,
    VatPercentage Percentage);

public record UnvalidatedVat(
    string? Code = null,
    decimal? Percentage = null);
