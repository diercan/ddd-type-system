using LanguageExt;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;

namespace Sample.FinanceSystem.Domain.Workflows.Common;
public abstract class Workflow<TInputEntity, TContext, TResultEntity, TEvent>
{
    protected IRepository<TInputEntity, TContext, TResultEntity> Repository { get; }
    protected IResultMapper<TResultEntity, TEvent> ResultMapper { get; }

    protected Workflow(
        IRepository<TInputEntity, TContext, TResultEntity> repository,
        IResultMapper<TResultEntity, TEvent> resultMapper)
    {
        Repository = repository;
        ResultMapper = resultMapper;
    }

    public Task<TEvent> RunAsync(TInputEntity input)
    {
        var result =
            from context in Repository.TryLoadDbContext(input)
                            .ToEither(ex => new UnexpectedErrorMessage(ex) as IErrorMessage)
            from validEntity in RunBusinessRules(input, context)
                            .ToAsync()
            from unit in Repository.TrySaveToDb(validEntity)
                        .ToEither(ex => new UnexpectedErrorMessage(ex) as IErrorMessage)
            select validEntity;

        return ResultMapper.ResultToEvent(result);
    }

    protected abstract Either<IErrorMessage, TResultEntity> RunBusinessRules(TInputEntity inputEntity, TContext context);


}
