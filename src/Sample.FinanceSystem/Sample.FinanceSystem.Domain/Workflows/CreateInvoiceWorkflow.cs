using LanguageExt;
using Sample.FinanceSystem.Api.Models;
using Sample.FinanceSystem.Domain.Operations.Calculations;
using Sample.FinanceSystem.Domain.Types;
using Sample.FinanceSystem.Domain.Workflows.Common;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;
using static Sample.FinanceSystem.Domain.Types.InvoiceCreatedEvent;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;
using static LanguageExt.Prelude;

namespace Sample.FinanceSystem.Domain.Workflows;
public class CreateInvoiceWorkflow : Workflow<IInvoiceEntity, InvoiceDto, InvoiceContext, IInvoiceCreatedEvent>
{
    protected override Task<IInvoiceCreatedEvent> ConvertResultToEvent<CalculatedInvoice>(EitherAsync<IErrorMessage, CalculatedInvoice> result)
    {
        throw new NotImplementedException();
    }

    protected override Either<IErrorMessage, UnvalidatedInvoice> MapDtoToEntity<UnvalidatedInvoice>(InvoiceDto input)
    {
        throw new NotImplementedException();
    }

    protected override Either<IErrorMessage, CalculatedInvoice> RunBusinessRules<CalculatedInvoice, UnvalidatedInvoice>(UnvalidatedInvoice inputEntity, InvoiceContext context)
    {
        // The generic here is only of type IInvoiceEntity, not the more specific UnvalidatedInvoice.
        InvoiceEntity.UnvalidatedInvoice unvalidatedInvoice = inputEntity as InvoiceEntity.UnvalidatedInvoice;

        var result = from dueDate in Try(() => new CalculateDueDateOperation().Run(unvalidatedInvoice, context))
                         // dueDate here is still an invoice, not just the due date.
                         // What if each operation creates another instance of the UnvalidatedInvoice that is passed to the following operation?
                         // What if CalculatedInvoice and UnvalidatedInvoice would have the same fields, with the difference that the Calculated one is also validated?
                     select dueDate;

        throw new NotImplementedException();
    }

    protected override TryAsync<InvoiceContext> TryLoadDbContext<UnvalidatedInvoice>(UnvalidatedInvoice entity)
    {
        throw new NotImplementedException();
    }

    protected override TryAsync<Unit> TrySaveToDb<CalculatedInvoice>(CalculatedInvoice entity)
    {
        throw new NotImplementedException();
    }
}
