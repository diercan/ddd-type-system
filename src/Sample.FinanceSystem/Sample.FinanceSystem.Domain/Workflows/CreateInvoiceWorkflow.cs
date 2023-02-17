using LanguageExt;
using Sample.FinanceSystem.Domain.Operations.Calculations;
using Sample.FinanceSystem.Domain.Operations.Validations;
using Sample.FinanceSystem.Domain.Types;
using Sample.FinanceSystem.Domain.Workflows.Common;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;
using static Sample.FinanceSystem.Domain.Types.InvoiceCreatedEvent;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Domain.Workflows;
public class CreateInvoiceWorkflow : Workflow<UnvalidatedInvoice, InvoiceContext, ValidatedInvoice, IInvoiceCreatedEvent>
{
    public CreateInvoiceWorkflow(
        IRepository<UnvalidatedInvoice, InvoiceContext, ValidatedInvoice> repository,
        IResultMapper<ValidatedInvoice, IInvoiceCreatedEvent> resultMapper) : base(repository, resultMapper) { }

    protected override EitherAsync<IErrorMessage, ValidatedInvoice> RunBusinessRules(UnvalidatedInvoice inputEntity, InvoiceContext context)
        => from invoiceWithDefaults in CalculateInvoiceDefaults(inputEntity, context)
           from validInvoice in ValidateInvoice(invoiceWithDefaults, context)
           from calculatedInvoice in CalculateInvoiceTotals(validInvoice, context)
           select calculatedInvoice;

    private static EitherAsync<IErrorMessage, UnvalidatedInvoice> ValidateInvoice(UnvalidatedInvoice invoice, InvoiceContext context)
        => new ValidateInvoiceOperation().Run(invoice, context);
    private static EitherAsync<IErrorMessage, UnvalidatedInvoice> CalculateInvoiceDefaults(UnvalidatedInvoice invoice, InvoiceContext context)
        => new CalculateInvoiceDefaultsOperation().Run(invoice, context);
    private static EitherAsync<IErrorMessage, ValidatedInvoice> CalculateInvoiceTotals(UnvalidatedInvoice invoice, InvoiceContext context)
        => new CalculateInvoiceTotalsOperation().Run(invoice, context);


    protected static EitherAsync<IErrorMessage, ValidatedInvoice> RunBusinessRules_2(UnvalidatedInvoice inputEntity, InvoiceContext context)
        => from invoiceWithDueDate in CalculateDueDate(inputEntity, context)
           from invoiceWithCurrency in CalculateDefaultCurrency(invoiceWithDueDate, context)
           from invoiceWithVatPercentage in CalculateVatPercentage(invoiceWithCurrency, context)
           from invoiceWithDetailLines in CalculateDetailLinesTotal(invoiceWithVatPercentage, context)
           from invoiceWithTotal in CalculateInvoiceTotal(invoiceWithDetailLines, context)
           from calculatedInvoice in ValidateCalculatedInvoice(invoiceWithTotal, context)
           select calculatedInvoice;

    private static EitherAsync<IErrorMessage, UnvalidatedInvoice> CalculateDueDate(UnvalidatedInvoice invoice, InvoiceContext context)
    => new CalculateDueDateOperation().Run(invoice, context);
    private static EitherAsync<IErrorMessage, UnvalidatedInvoice> CalculateDefaultCurrency(UnvalidatedInvoice invoice, InvoiceContext context)
        => new CalculateDefaultCurrencyOperation().Run(invoice, context);
    private static EitherAsync<IErrorMessage, UnvalidatedInvoice> CalculateVatPercentage(UnvalidatedInvoice invoice, InvoiceContext context)
        => new CalculateVatPercentageOperation().Run(invoice, context);
    private static EitherAsync<IErrorMessage, UnvalidatedInvoice> CalculateDetailLinesTotal(UnvalidatedInvoice invoice, InvoiceContext context)
        => new CalculateDetailLinesTotalOperation().Run(invoice, context);
    private static EitherAsync<IErrorMessage, UnvalidatedInvoice> CalculateInvoiceTotal(UnvalidatedInvoice invoice, InvoiceContext context)
        => new CalculateInvoiceTotalOperation().Run(invoice, context);
    private static EitherAsync<IErrorMessage, ValidatedInvoice> ValidateCalculatedInvoice(UnvalidatedInvoice invoice, InvoiceContext context)
        => new ValidateCalculatedInvoiceOperation().Run(invoice, context);
}
