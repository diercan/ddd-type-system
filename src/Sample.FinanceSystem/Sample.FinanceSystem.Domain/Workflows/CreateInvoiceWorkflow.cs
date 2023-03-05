using LanguageExt;
using Sample.FinanceSystem.Domain.Operations.Calculations;
using Sample.FinanceSystem.Domain.Operations.Common;
using Sample.FinanceSystem.Domain.Operations.Validations;
using Sample.FinanceSystem.Domain.Types;
using Sample.FinanceSystem.Domain.Workflows.Common;
using static LanguageExt.Prelude;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;
using static Sample.FinanceSystem.Domain.Types.InvoiceCreatedEvent;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Domain.Workflows;
public class CreateInvoiceWorkflow : Workflow<UnvalidatedInvoice, InvoiceContext, ValidatedInvoice, IInvoiceCreatedEvent>
{
    public CreateInvoiceWorkflow(
        IRepository<UnvalidatedInvoice, InvoiceContext, ValidatedInvoice> repository,
        IResultMapper<ValidatedInvoice, IInvoiceCreatedEvent> resultMapper) : base(repository, resultMapper) { }

    protected override EitherAsync<IErrorMessage, ValidatedInvoice> RunBusinessRules(UnvalidatedInvoice inputEntity, InvoiceContext context) => inputEntity
        .RunIfMatch(context, new CalculateDefaultCreationDate())
        .RunIfMatch(context, new CalculateDefaultCustomerDetailsOperation())
        .RunIfMatch(context, new CalculateDueDateOperation())
        .RunIfMatch(context, new CalculateDefaultCurrencyOperation())
        .RunIfMatch(context, new CalculateVatPercentageOperation())
        .RunIfMatch(context, new CalculateDetailLinesTotalOperation())
        .RunIfMatch(context, new CalculateInvoiceTotalOperation())
        .RunIfMatch(context, new ValidateCalculatedInvoiceOperation())
        .Match<Either<IErrorMessage, ValidatedInvoice>>(
            whenApprovedInvoice: invoice => Left<IErrorMessage>(new UnexpectedErrorMessage("Approved invoice is an invalid state")),
            whenInvalidInvoice: invoice => Left(invoice.ErrorMessage),
            whenPaidInvoice: invoice => Left<IErrorMessage>(new UnexpectedErrorMessage("Paid invoice is an invalid state")),
            whenUnvalidatedInvoice: invoice => Left<IErrorMessage>(new UnexpectedErrorMessage("Unvalidated invoice is an invalid state")),
            whenValidatedInvoice: invoice => invoice)
        .ToAsync();
}
