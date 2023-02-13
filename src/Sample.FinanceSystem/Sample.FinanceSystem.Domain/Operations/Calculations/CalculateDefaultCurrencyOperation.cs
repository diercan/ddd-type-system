using Sample.FinanceSystem.Domain.Operations.Common;
using Sample.FinanceSystem.Domain.Types;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Domain.Operations.Calculations;

internal class CalculateDefaultCurrencyOperation : InvoiceOperation<UnvalidatedInvoice>
{
    public override IInvoiceEntity Run(UnvalidatedInvoice input, InvoiceContext context)
    {
        return input switch
        {
            { Currency: not null } => input,
            _ => input with { Currency = context.CustomerCurrency }
        };
    }
}
