using LanguageExt;

namespace Sample.FinanceSystem.Domain.Workflows.Common;
public interface IRepository<TInputEntity, TContext, TResultEntity>
{
    TryAsync<TContext> TryLoadDbContext(TInputEntity entity);
    TryAsync<Unit> TrySaveToDb(TResultEntity entity);
}
