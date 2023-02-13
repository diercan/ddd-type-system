using LanguageExt;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;

namespace Sample.FinanceSystem.Domain.Workflows.Common;
public abstract class Workflow<TEntity, TValidEntity, TUnvalidatedEntity, TInputDto, TContext, TEvent>
        where TUnvalidatedEntity : TEntity
        where TValidEntity : TEntity
{
    protected abstract Either<IErrorMessage, TUnvalidatedEntity> MapDtoToEntity(TInputDto input);
    protected abstract TryAsync<TContext> TryLoadDbContext(TUnvalidatedEntity entity);
    protected abstract Either<IErrorMessage, TValidEntity> RunBusinessRules(TUnvalidatedEntity inputEntity, TContext context);

    protected abstract TryAsync<Unit> TrySaveToDb(TValidEntity entity);

    protected abstract Task<TEvent> ConvertResultToEvent(EitherAsync<IErrorMessage, TValidEntity> result);

    public Task<TEvent> RunAsync(TInputDto input)
    {
        var result =
            from unvalidatedEntity in MapDtoToEntity(input).ToAsync()
            from context in TryLoadDbContext(unvalidatedEntity)
                            .ToEither(ex => new UnexpectedErrorMessage(ex) as IErrorMessage)
            from validEntity in RunBusinessRules(unvalidatedEntity, context)
                            .ToAsync()
            from unit in TrySaveToDb(validEntity)
                        .ToEither(ex => new UnexpectedErrorMessage(ex) as IErrorMessage)
            select validEntity;

        return ConvertResultToEvent(result);
    }
}
