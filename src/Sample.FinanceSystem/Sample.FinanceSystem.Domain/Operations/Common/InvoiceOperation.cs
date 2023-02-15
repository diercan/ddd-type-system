using Sample.FinanceSystem.Domain.Types;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Domain.Operations.Common;
public abstract class InvoiceOperation<TInvoice> : Operation<TInvoice, IInvoice, InvoiceContext> where TInvoice : IInvoice
{
}

public abstract class InvoiceOperation2<TInvoice> : Operation2<TInvoice, IInvoice, InvoiceContext> where TInvoice : IInvoice
{
}
