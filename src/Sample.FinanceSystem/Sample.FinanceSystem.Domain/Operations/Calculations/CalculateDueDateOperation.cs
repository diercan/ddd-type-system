using Sample.FinanceSystem.Domain.Operations.Common;
using Sample.FinanceSystem.Domain.Types;
using static Sample.FinanceSystem.Domain.Types.Invoice;

namespace Sample.FinanceSystem.Domain.Operations.Calculations;
internal class CalculateDueDateOperation : InvoiceOperation<UnvalidatedInvoice>
{
    public override IInvoices Run(UnvalidatedInvoice input, InvoiceContext context)
    {
        throw new NotImplementedException();
    }
}
