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

    //protected override EitherAsync<IErrorMessage, ValidatedInvoice> RunBusinessRules(UnvalidatedInvoice inputEntity, InvoiceContext context)
    //    => from invoiceWithDefaults in CalculateInvoiceDefaults(inputEntity, context)
    //       from validInvoice in ValidateInvoice(invoiceWithDefaults, context)
    //       from calculatedInvoice in CalculateInvoiceTotals(validInvoice, context)
    //       select calculatedInvoice;

    //private static EitherAsync<IErrorMessage, UnvalidatedInvoice> ValidateInvoice(UnvalidatedInvoice invoice, InvoiceContext context)
    //    => new ValidateInvoiceOperation().Run(invoice, context);
    //private static EitherAsync<IErrorMessage, UnvalidatedInvoice> CalculateInvoiceDefaults(UnvalidatedInvoice invoice, InvoiceContext context)
    //    => new CalculateInvoiceDefaultsOperation().Run(invoice, context);
    //private static EitherAsync<IErrorMessage, ValidatedInvoice> CalculateInvoiceTotals(UnvalidatedInvoice invoice, InvoiceContext context)
    //    => new CalculateInvoiceTotalsOperation().Run(invoice, context);

    protected override EitherAsync<IErrorMessage, ValidatedInvoice> RunBusinessRules(UnvalidatedInvoice inputEntity, InvoiceContext context)
        => from invoiceWithDefaults in CalculateInvoiceDefaults(inputEntity, context)
           from invoiceWithTotal in CalculateInvoiceTotals(invoiceWithDefaults, context)
           from calculatedInvoice in ValidateCalculatedInvoice(invoiceWithTotal, context)
           select calculatedInvoice;

    protected static EitherAsync<IErrorMessage, UnvalidatedInvoice> CalculateInvoiceDefaults(UnvalidatedInvoice invoice, InvoiceContext context)
        => from invoiceWithCreationDate in CalculateDefaultCreationDate(invoice, context)
           from invoiceWithCustomer in CalculateDefaultCustomerDetails(invoiceWithCreationDate, context)
           from invoiceWithCurrency in CalculateDefaultCurrency(invoiceWithCustomer, context)
           from invoiceWithVatPercentage in CalculateVatPercentage(invoiceWithCurrency, context)
           from invoiceWithDueDate in CalculateDueDate(invoiceWithVatPercentage, context)
           select invoiceWithDueDate;

    protected static EitherAsync<IErrorMessage, UnvalidatedInvoice> CalculateInvoiceTotals(UnvalidatedInvoice invoice, InvoiceContext context)
        => from invoiceWithDetailLines in CalculateDetailLinesTotal(invoice, context)
           from invoiceWithTotal in CalculateInvoiceTotal(invoiceWithDetailLines, context)
           select invoiceWithTotal;

    private static EitherAsync<IErrorMessage, UnvalidatedInvoice> CalculateDefaultCreationDate(UnvalidatedInvoice invoice, InvoiceContext context)
        => new CalculateDefaultCreationDate().Run(invoice, context);
    private static EitherAsync<IErrorMessage, UnvalidatedInvoice> CalculateDefaultCustomerDetails(UnvalidatedInvoice invoice, InvoiceContext context)
        => new CalculateDefaultCustomerDetailsOperation().Run(invoice, context);
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
