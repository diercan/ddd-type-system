using LanguageExt;
using Sample.FinanceSystem.Domain.Operations.Calculations;
using Sample.FinanceSystem.Domain.Operations.Common;
using Sample.FinanceSystem.Domain.Operations.Validations;
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
        IInvoice calculatedInvoice = inputEntity
            .RunIfMatch(context, new ValidatePreCalculationOperation())
            .RunIfMatch(context, new CalculateDefaultCurrencyOperation())
            .RunIfMatch(context, new CalculateDefaultVatPercentage())
            .RunIfMatch(context, new ValidateDefaultsOperation())
            .RunIfMatch(context, new CalculateDueDateOperation())
            .RunIfMatch(context, new CalculateDetailLinesTotalOperation())
            .RunIfMatch(context, new CalculateInvoiceTotalOperation())
            .RunIfMatch(context, new ValidatePostCalculationOperation());

        return calculatedInvoice switch
        {
            CalculatedInvoice calculated => (Either<IErrorMessage, CalculatedInvoice>)calculated,
            _ => (Either<IErrorMessage, CalculatedInvoice>)new UnexpectedErrorMessage($"Unexpected invoice state {calculatedInvoice.GetType().Name}"),
        };
    }
}
