using LanguageExt;
using Sample.FinanceSystem.Api.Models;
using Sample.FinanceSystem.Domain.Types;
using Sample.FinanceSystem.Domain.Workflows.Common;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;
using static Sample.FinanceSystem.Domain.Types.InvoiceCreatedEvent;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

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
