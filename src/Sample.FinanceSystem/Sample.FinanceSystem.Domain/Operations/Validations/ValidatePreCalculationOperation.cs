using Sample.FinanceSystem.Domain.Operations.Common;
using Sample.FinanceSystem.Domain.Types;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Domain.Operations.Validations;

internal class ValidatePreCalculationOperation : InvoiceOperation<UnvalidatedInvoice>
{
    public override IInvoice Run(UnvalidatedInvoice input, InvoiceContext context)
    {
        // Todo: Run all pre calculations validations. If any errors found return InvalidInvoice
        IEnumerable<ValidationError> errors = Enumerable.Empty<ValidationError>();

        return errors.Any()
            ? new InvalidInvoice(input.Customer, input.CreationDate, errors, input.Lines)
            : input;
    }
}
