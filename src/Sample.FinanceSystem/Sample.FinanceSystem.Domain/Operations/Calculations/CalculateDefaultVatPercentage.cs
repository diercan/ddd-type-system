using Sample.FinanceSystem.Domain.Operations.Common;
using Sample.FinanceSystem.Domain.Types;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Domain.Operations.Calculations;

internal class CalculateDefaultVatPercentage : InvoiceOperation<UnvalidatedInvoice>
{
    public override IInvoice Run(UnvalidatedInvoice input, InvoiceContext context)
    {
        // This operation will calculate the VAT percentage based on the VAT code for each Invoice line if not already specified
        throw new NotImplementedException();
    }
}
