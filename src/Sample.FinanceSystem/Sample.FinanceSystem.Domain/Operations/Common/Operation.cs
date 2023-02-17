using LanguageExt;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;

namespace Sample.FinanceSystem.Domain.Operations.Common;

public abstract class Operation<TInputEntity, TOutputEntity, TInterfaceEntity, TContext>
    where TInputEntity : TInterfaceEntity
    where TOutputEntity : TInterfaceEntity
{
    public abstract EitherAsync<IErrorMessage, TOutputEntity> Run(TInputEntity input, TContext context);
}
