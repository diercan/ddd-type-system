using LanguageExt;
using Sample.FinanceSystem.Domain.Operations.Common;
using Sample.FinanceSystem.Domain.Types;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Domain.Operations.Calculations
{
    internal class CalculateInvoiceTotalsOperation : InvoiceOperation<UnvalidatedInvoice, ValidatedInvoice>
    {
        public override EitherAsync<IErrorMessage, ValidatedInvoice> Run(UnvalidatedInvoice input, InvoiceContext context)
        {
            // Todo: Aggregates all calculate operations.
            throw new NotImplementedException();
        }
    }
}
