using LanguageExt;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;

namespace Sample.FinanceSystem.Domain.Workflows.Common;
public interface IResultMapper<TResultEntity, TEvent>
{
    Task<TEvent> ResultToEvent(EitherAsync<IErrorMessage, TResultEntity> result);
}
