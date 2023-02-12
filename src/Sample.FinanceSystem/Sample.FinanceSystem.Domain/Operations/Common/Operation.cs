namespace Sample.FinanceSystem.Domain.Operations.Common;
public abstract class Operation<TInputEntity, TInterfaceEntity, TContext>
    where TInputEntity : TInterfaceEntity
{
    public abstract TInterfaceEntity Run(TInputEntity input, TContext context);
}
