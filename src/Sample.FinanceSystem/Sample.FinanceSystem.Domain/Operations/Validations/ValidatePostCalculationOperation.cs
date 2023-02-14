using Sample.FinanceSystem.Domain.Operations.Common;
using Sample.FinanceSystem.Domain.Types;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Domain.Operations.Validations;

internal class ValidatePostCalculationOperation : InvoiceOperation<UnvalidatedInvoice>
{
    public override IInvoice Run(UnvalidatedInvoice input, InvoiceContext context)
    {
        // Todo: Run all validations post calculations to see if this is valid invoice.
        IEnumerable<ValidationError> errors = Enumerable.Empty<ValidationError>();

        return errors.Any()
            ? new InvalidInvoice(input.Customer, input.CreationDate, errors, input.Lines)
            : new CalculatedInvoice(input.Customer, input.CreationDate, input.DueDate.Value, input.Total, input.Lines);
    }
}
