using Sample.FinanceSystem.Domain.Types;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Domain.Operations.Common;
public abstract class InvoiceOperation<TInvoice> : Operation<TInvoice, IInvoiceEntity, InvoiceContext> where TInvoice : IInvoiceEntity
{
}
