using Sample.FinanceSystem.Domain.Types.DetailLineTypes;
using Sample.FinanceSystem.Domain.Types.LedgerCodeTypes;
using Sample.FinanceSystem.Domain.Types.VatTypes;

namespace Sample.FinanceSystem.Domain.Types.InvoiceDetailLineTypes;

public record DetailLine(
    DetailLineDescription Description,
    LedgerCode LedgerCode,
    Vat Vat,
    DetailLineTotal Total);

public record UnvalidatedDetailLine(
    string? Description,
    UnvalidatedLedgerCode LedgerCode,
    UnvalidatedVat Vat,
    UnvalidatedDetailLineTotal Total);
