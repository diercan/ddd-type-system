using Sample.FinanceSystem.Domain.Operations.Common;
using Sample.FinanceSystem.Domain.Types;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Domain.Operations;
public static class InvoiceOperationExecution
{
    public static IInvoice RunIfUnvalidatedInvoice<TOperation>(this IInvoice entity, InvoiceContext context)
        where TOperation : InvoiceOperation<UnvalidatedInvoice>, new() =>
        entity.RunIfMatch(context, new TOperation());

    public static IInvoice RunIfCalculatedInvoice<TOperation>(this IInvoice entity, InvoiceContext context)
        where TOperation : InvoiceOperation<CalculatedInvoice>, new() =>
        entity.RunIfMatch(context, new TOperation());
};
