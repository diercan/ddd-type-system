using LanguageExt;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Domain.Operations.Common;

public abstract class Operation<TInputEntity, TInterfaceEntity, TContext>
    where TInputEntity : TInterfaceEntity
{
    public abstract TInterfaceEntity Run(TInputEntity input, TContext context);
}

public abstract class Operation2<TInputEntity, TInterfaceEntity, TContext>
    where TInputEntity : TInterfaceEntity
{
    public EitherAsync<IErrorMessage, TInterfaceEntity> Run(TInterfaceEntity input, TContext context)
    {
        return input switch
        {
            TInputEntity ofType => Run(ofType, context),
            _ => input
        };
    }

    protected abstract EitherAsync<IErrorMessage, TInterfaceEntity> Run(TInputEntity input, TContext context);
}
