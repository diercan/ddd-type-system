using LanguageExt;
using Sample.FinanceSystem.Domain.Operations.Common;
using Sample.FinanceSystem.Domain.Types;
using Sample.FinanceSystem.Domain.Types.Common;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Domain.Operations.Calculations;

internal class CalculateDefaultCurrencyOperation : InvoiceOperation<UnvalidatedInvoice, UnvalidatedInvoice>
{
    public override EitherAsync<ErrorMessage.IErrorMessage, UnvalidatedInvoice> Run(UnvalidatedInvoice input, InvoiceContext context)
    {
        return input switch
        {
            { Currency: not null } => input,
            _ => input with { Currency = context.CustomerCurrency }
        };
    }
}
