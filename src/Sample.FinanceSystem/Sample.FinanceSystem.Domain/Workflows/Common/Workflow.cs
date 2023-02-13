using LanguageExt;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;

namespace Sample.FinanceSystem.Domain.Workflows.Common;
public abstract class Workflow<TEntity, TInputDto, TContext, TEvent>
{
    protected abstract Either<IErrorMessage, TUnvalidatedEntity> MapDtoToEntity<TUnvalidatedEntity>(TInputDto input)
        where TUnvalidatedEntity : TEntity;
    protected abstract TryAsync<TContext> TryLoadDbContext<TUnvalidatedEntity>(TUnvalidatedEntity entity)
        where TUnvalidatedEntity : TEntity;
    protected abstract Either<IErrorMessage, TValidEntity> RunBusinessRules<TValidEntity, TUnvalidatedEntity>(TUnvalidatedEntity inputEntity, TContext context)
        where TUnvalidatedEntity : TEntity
        where TValidEntity : TEntity;
    protected abstract TryAsync<Unit> TrySaveToDb<TValidEntity>(TValidEntity entity)
        where TValidEntity : TEntity;

    protected abstract Task<TEvent> ConvertResultToEvent<TValidEntity>(EitherAsync<IErrorMessage, TValidEntity> result)
        where TValidEntity : TEntity;

    public Task<TEvent> RunAsync<TValidEntity, TUnvalidatedEntity>(TInputDto input)
        where TUnvalidatedEntity : TEntity
        where TValidEntity : TEntity
    {
        var result =
            from unvalidatedEntity in MapDtoToEntity<TUnvalidatedEntity>(input).ToAsync()
            from context in TryLoadDbContext(unvalidatedEntity)
                            .ToEither(ex => new UnexpectedErrorMessage(ex) as IErrorMessage)
            from validEntity in RunBusinessRules<TValidEntity, TUnvalidatedEntity>(unvalidatedEntity, context)
                            .ToAsync()
            from unit in TrySaveToDb(validEntity)
                        .ToEither(ex => new UnexpectedErrorMessage(ex) as IErrorMessage)
            select validEntity;

        return ConvertResultToEvent(result);
    }
}
