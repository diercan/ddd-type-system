using LanguageExt;
using Sample.FinanceSystem.Domain.Operations;
using Sample.FinanceSystem.Domain.Operations.Calculations;
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
        .RunIf<UnvalidatedInvoice, CalculateDefaultCreationDate>(context)
        .RunIf<UnvalidatedInvoice, CalculateDefaultCustomerDetailsOperation>(context)
        .RunIf<UnvalidatedInvoice, CalculateDueDateOperation>(context)
        .RunIf<UnvalidatedInvoice, CalculateDefaultCurrencyOperation>(context)
        .RunIf<UnvalidatedInvoice, CalculateVatPercentageOperation>(context)
        .RunIf<UnvalidatedInvoice, CalculateDetailLinesTotalOperation>(context)
        .RunIf<UnvalidatedInvoice, CalculateInvoiceTotalOperation>(context)
        .RunIf<UnvalidatedInvoice, ValidateCalculatedInvoiceOperation>(context)
        .Match(
            whenValidatedInvoice: invoice => invoice,
            whenInvalidInvoice: invoice => Left(invoice.ErrorMessage),
            whenApprovedInvoice: invoice => GenerateInvalidStateErrorMessage("Approved"),
            whenPaidInvoice: invoice => GenerateInvalidStateErrorMessage("Paid"),
            whenUnvalidatedInvoice: (Func<UnvalidatedInvoice, Either<IErrorMessage, ValidatedInvoice>>)(invoice => GenerateInvalidStateErrorMessage("Unvalidated")))
        .ToAsync();

    private static EitherLeft<IErrorMessage> GenerateInvalidStateErrorMessage(string stateName) =>
        Left<IErrorMessage>(new UnexpectedErrorMessage($"{stateName} invoice is an invalid state"));
}
