namespace Sample.FinanceSystem.Domain.Operations.Common;
public static class OperationExecution
{
    public static TInterfaceEntity RunIfMatch<TInterfaceEntity, TInputEntity, TContext>(
        this TInterfaceEntity entity,
        TContext context,
        Operation<TInputEntity, TInterfaceEntity, TContext> operation)
        where TInputEntity : TInterfaceEntity =>
        entity switch
        {
            TInputEntity input => operation.Run(input, context),
            _ => entity
        };
}