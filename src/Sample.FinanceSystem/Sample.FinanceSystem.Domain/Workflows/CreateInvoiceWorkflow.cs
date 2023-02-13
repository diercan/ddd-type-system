using LanguageExt;
using Sample.FinanceSystem.Domain.Types;
using Sample.FinanceSystem.Domain.Workflows.Common;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;
using static Sample.FinanceSystem.Domain.Types.InvoiceCreatedEvent;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Domain.Workflows;
public class CreateInvoiceWorkflow : Workflow<UnvalidatedInvoice, InvoiceContext, CalculatedInvoice, IInvoiceCreatedEvent>
{
    public CreateInvoiceWorkflow(
        IRepository<UnvalidatedInvoice, InvoiceContext, CalculatedInvoice> repository,
        IResultMapper<CalculatedInvoice, IInvoiceCreatedEvent> resultMapper) : base(repository, resultMapper) { }

    protected override Either<IErrorMessage, CalculatedInvoice> RunBusinessRules(UnvalidatedInvoice inputEntity, InvoiceContext context)
    {
        throw new NotImplementedException();
    }
}
