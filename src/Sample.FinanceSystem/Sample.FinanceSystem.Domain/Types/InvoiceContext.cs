using Sample.FinanceSystem.Domain.Types.CustomerTypes;
using Sample.FinanceSystem.Domain.Types.VatTypes;

namespace Sample.FinanceSystem.Domain.Types;

public record InvoiceContext(
    CustomerContext CustomerContext,
    IReadOnlyList<VatContext> VatContext);