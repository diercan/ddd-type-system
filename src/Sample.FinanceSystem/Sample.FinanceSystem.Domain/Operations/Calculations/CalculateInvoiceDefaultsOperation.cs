using LanguageExt;
using Sample.FinanceSystem.Domain.Operations.Common;
using Sample.FinanceSystem.Domain.Types;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Domain.Operations.Calculations
{
    internal class CalculateInvoiceDefaultsOperation : InvoiceOperation2<UnvalidatedInvoice>
    {
        protected override EitherAsync<IErrorMessage, IInvoice> Run(UnvalidatedInvoice input, InvoiceContext context)
        {
            // Todo: Aggregates all calculate default operations
            throw new NotImplementedException();
        }
    }
}
