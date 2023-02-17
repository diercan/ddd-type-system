using LanguageExt;
using Sample.FinanceSystem.Domain.Operations.Common;
using Sample.FinanceSystem.Domain.Types;
using Sample.FinanceSystem.Domain.Types.Common;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Domain.Operations.Calculations;

internal class CalculateInvoiceTotalOperation : InvoiceOperation<UnvalidatedInvoice, UnvalidatedInvoice>
{
    public override EitherAsync<ErrorMessage.IErrorMessage, UnvalidatedInvoice> Run(UnvalidatedInvoice input, InvoiceContext context)
    {
        throw new NotImplementedException();
    }
}
