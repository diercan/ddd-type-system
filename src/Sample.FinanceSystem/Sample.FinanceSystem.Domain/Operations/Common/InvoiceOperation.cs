using LanguageExt;
using Sample.FinanceSystem.Domain.Types;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Domain.Operations.Common;
public abstract class InvoiceOperation<TInInvoice, TOutInvoice> : Operation<TInInvoice, TOutInvoice, IInvoice, InvoiceContext>
    where TInInvoice : IInvoice
    where TOutInvoice : IInvoice
{ }