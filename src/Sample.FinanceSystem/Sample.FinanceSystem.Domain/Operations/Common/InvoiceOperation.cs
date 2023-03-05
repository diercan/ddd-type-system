using Sample.FinanceSystem.Domain.Types;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Domain.Operations.Common;
public abstract class InvoiceOperation<TInInvoice> : Operation<TInInvoice, IInvoice, InvoiceContext>
    where TInInvoice : IInvoice
{ }