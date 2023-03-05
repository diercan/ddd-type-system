using Sample.FinanceSystem.Domain.Operations.Common;
using Sample.FinanceSystem.Domain.Types;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Domain.Operations.Validations
{
    internal class ValidateCalculatedInvoiceOperation : InvoiceOperation<UnvalidatedInvoice>
    {
        public override IInvoice Run(UnvalidatedInvoice input, InvoiceContext context)
        {
            throw new NotImplementedException();
        }
    }
}
