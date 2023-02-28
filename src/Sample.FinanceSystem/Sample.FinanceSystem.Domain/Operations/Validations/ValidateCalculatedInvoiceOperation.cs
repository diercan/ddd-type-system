using LanguageExt;
using Sample.FinanceSystem.Domain.Operations.Common;
using Sample.FinanceSystem.Domain.Types;
using Sample.FinanceSystem.Domain.Types.Common;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Domain.Operations.Validations
{
    internal class ValidateCalculatedInvoiceOperation : InvoiceOperation<UnvalidatedInvoice, ValidatedInvoice>
    {
        public override EitherAsync<ErrorMessage.IErrorMessage, ValidatedInvoice> Run(UnvalidatedInvoice input, InvoiceContext context)
        {
            throw new NotImplementedException();
        }
    }
}
