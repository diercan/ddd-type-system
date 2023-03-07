using Sample.FinanceSystem.Domain.Operations.Common;
using Sample.FinanceSystem.Domain.Types;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Domain.Operations;
public static class InvoiceOperations
{
    public static IInvoice RunIf<TInvoice, TOperation>(
        this IInvoice entity,
        InvoiceContext context)
        where TInvoice : IInvoice
        where TOperation : Operation<TInvoice, IInvoice, InvoiceContext>, new() =>
        entity.RunIfMatch(context, new TOperation());
}
