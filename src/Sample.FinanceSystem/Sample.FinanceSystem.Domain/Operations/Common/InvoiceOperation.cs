using Sample.FinanceSystem.Domain.Types;
using static Sample.FinanceSystem.Domain.Types.Invoice;

namespace Sample.FinanceSystem.Domain.Operations.Common;
public abstract class InvoiceOperation<TInvoice> : Operation<TInvoice, IInvoices, InvoiceContext> where TInvoice : IInvoices
{
}
