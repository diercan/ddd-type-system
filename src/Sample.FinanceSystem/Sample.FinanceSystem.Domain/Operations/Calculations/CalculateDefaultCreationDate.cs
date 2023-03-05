using Sample.FinanceSystem.Domain.Operations.Common;
using Sample.FinanceSystem.Domain.Types;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Domain.Operations.Calculations;

internal class CalculateDefaultCreationDate : InvoiceOperation<UnvalidatedInvoice>
{
    public override IInvoice Run(UnvalidatedInvoice input, InvoiceContext context)
    {
        return input.CreationDate == null
            ? input with { CreationDate = DateOnly.FromDateTime(DateTime.Now) }
            : input;
    }
}
