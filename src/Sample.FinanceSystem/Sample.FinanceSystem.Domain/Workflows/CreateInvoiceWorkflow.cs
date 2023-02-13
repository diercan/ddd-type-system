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
    protected override Task<IInvoiceCreatedEvent> ConvertResultToEvent<TValidEntity>(EitherAsync<IErrorMessage, TValidEntity> result)
    {
        throw new NotImplementedException();
    }

    protected override Either<IErrorMessage, TUnvalidatedEntity> MapDtoToEntity<TUnvalidatedEntity>(InvoiceDto input)
    {
        throw new NotImplementedException();
    }

    protected override Either<IErrorMessage, TValidEntity> RunBusinessRules<TValidEntity, TUnvalidatedEntity>(TUnvalidatedEntity inputEntity, InvoiceContext context)
    {
        throw new NotImplementedException();
    }

    protected override TryAsync<InvoiceContext> TryLoadDbContext<TUnvalidatedEntity>(TUnvalidatedEntity entity)
    {
        throw new NotImplementedException();
    }

    protected override TryAsync<Unit> TrySaveToDb<TValidEntity>(TValidEntity entity)
    {
        throw new NotImplementedException();
    }
}
