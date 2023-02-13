namespace Sample.FinanceSystem.Domain.Operations.Common;

// Todo: If we want to return parts of an invoice, I think we should also have
// the TOutputValue\TOutputEntity as generic.
//
// This will allow us to return parts of the Invoice instead of the entire invoice
//
// Although, it might be better for Unvalidated and Calculated to have the same fields and
// all Calculate operations to create a new instance of Unvalidated - this might be easier to worth with
// and keep the code clean.

public abstract class Operation<TInputEntity, TInterfaceEntity, TContext>
    where TInputEntity : TInterfaceEntity
{
    // Todo: If we want to be able to compose this we should use TryAsync or Either as the result of the operation.
    public abstract TInterfaceEntity Run(TInputEntity input, TContext context);
}
