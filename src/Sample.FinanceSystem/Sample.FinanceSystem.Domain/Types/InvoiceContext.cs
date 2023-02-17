using Sample.FinanceSystem.Domain.Types.MoneyTypes;

namespace Sample.FinanceSystem.Domain.Types;

public record InvoiceContext(Currency CustomerCurrency);

//Customer
//VAT

// Entities in Domain, not EF entities