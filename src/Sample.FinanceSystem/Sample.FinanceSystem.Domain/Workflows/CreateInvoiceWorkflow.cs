using LanguageExt;
using Sample.FinanceSystem.Domain.Operations.Calculations;
using Sample.FinanceSystem.Domain.Types;
using Sample.FinanceSystem.Domain.Workflows.Common;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;
using static Sample.FinanceSystem.Domain.Types.InvoiceCreatedEvent;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;
using static LanguageExt.Prelude;

namespace Sample.FinanceSystem.Domain.Workflows;
public class CreateInvoiceWorkflow : Workflow<UnvalidatedInvoice, InvoiceContext, CalculatedInvoice, IInvoiceCreatedEvent>
{
    public CreateInvoiceWorkflow(
        IRepository<UnvalidatedInvoice, InvoiceContext, CalculatedInvoice> repository,
        IResultMapper<CalculatedInvoice, IInvoiceCreatedEvent> resultMapper) : base(repository, resultMapper) { }

    protected override Either<IErrorMessage, CalculatedInvoice> RunBusinessRules(UnvalidatedInvoice inputEntity, InvoiceContext context)
    {
        // If all operations would return a Either<ValidationError, UnvalidatedInvoice> calling them here would be straight forward.
        var result = from withDueDate in Try(() => new CalculateDueDateOperation().Run(inputEntity, context))
                         // withDueDate here is still an invoice, not just the due date.
                         // If each operation creates another instance of the UnvalidatedInvoice that is passed to the following operation
                         // we could chain all calculate and validate operations here.
                     from withCurrency in Try(() => new CalculateDefaultCurrencyOperation().Run(withDueDate as UnvalidatedInvoice, context))
                     from withVatPercentage in Try(() => new CalculateVatPercentage().Run(withCurrency as InvoiceEntity.UnvalidatedInvoice, context))
                     from withDetailLines in Try(() => new CalculateVatPercentage().Run(withVatPercentage as InvoiceEntity.UnvalidatedInvoice, context))
                     from withTotalInvoice in Try(() => new CalculateVatPercentage().Run(withDetailLines as InvoiceEntity.UnvalidatedInvoice, context))

                     select withTotalInvoice;

        throw new NotImplementedException();
    }
}
