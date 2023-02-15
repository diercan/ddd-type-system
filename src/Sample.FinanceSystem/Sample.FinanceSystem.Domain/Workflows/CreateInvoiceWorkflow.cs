using LanguageExt;
using Sample.FinanceSystem.Domain.Operations.Calculations;
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

    protected override EitherAsync<IErrorMessage, CalculatedInvoice> RunBusinessRules(UnvalidatedInvoice inputEntity, InvoiceContext context)
        => from invoiceWithDueDate in CalculateDueDate(inputEntity, context)
           from invoiceWithCurrency in CalculateDefaultCurrency(invoiceWithDueDate, context)
           from invoiceWithVatPercentage in CalculateVatPercentage(invoiceWithCurrency, context)
           from invoiceWithDetailLines in CalculateDetailLinesTotal(invoiceWithVatPercentage, context)
           from invoiceWithTotal in CalculateInvoiceTotal(invoiceWithDetailLines, context)
           from calculatedInvoice in AsCalculatedInvoice(invoiceWithTotal)
           select calculatedInvoice;

    protected static EitherAsync<IErrorMessage, CalculatedInvoice> RunBusinessRules2(UnvalidatedInvoice inputEntity, InvoiceContext context)
        => from validInvoiceWithDefaults in CalculateInvoiceDefaults(inputEntity, context)          // FROM InvoiceRequest -> UnvalidatedInvoice
           from validInvoice in ValidateInvoice(validInvoiceWithDefaults, context)                  // FROM UnvalidatedInvoice -> ValidatedInvoice
           from calculatedInvoice in CalculateInvoiceTotals(validInvoiceWithDefaults, context)      // FROM ValidatedInvoice -> CalculatedInvoice
           from ofCalculatedType in AsCalculatedInvoice(calculatedInvoice)                          // If operations have input\output type we don't need this.
           select ofCalculatedType;

    private static EitherAsync<IErrorMessage, IInvoice> ValidateInvoice(IInvoice invoice, InvoiceContext context)
        => new ValidateInvoiceOperation().Run(invoice, context);
    private static EitherAsync<IErrorMessage, IInvoice> CalculateInvoiceDefaults(IInvoice invoice, InvoiceContext context)
        => new CalculateInvoiceDefaultsOperation().Run(invoice, context);
    private static EitherAsync<IErrorMessage, IInvoice> CalculateInvoiceTotals(IInvoice invoice, InvoiceContext context)
    => new CalculateInvoiceTotalsOperation().Run(invoice, context);

    private static EitherAsync<IErrorMessage, IInvoice> CalculateDueDate(IInvoice invoice, InvoiceContext context)
        => new CalculateDueDateOperation().Run(invoice, context);

    private static EitherAsync<IErrorMessage, IInvoice> CalculateDefaultCurrency(IInvoice invoice, InvoiceContext context)
        => new CalculateDefaultCurrencyOperation().Run(invoice, context);

    private static EitherAsync<IErrorMessage, IInvoice> CalculateVatPercentage(IInvoice invoice, InvoiceContext context)
        => new CalculateVatPercentageOperation().Run(invoice, context);

    private static EitherAsync<IErrorMessage, IInvoice> CalculateDetailLinesTotal(IInvoice invoice, InvoiceContext context)
        => new CalculateDetailLinesTotalOperation().Run(invoice, context);

    private static EitherAsync<IErrorMessage, IInvoice> CalculateInvoiceTotal(IInvoice invoice, InvoiceContext context)
        => new CalculateInvoiceTotalOperation().Run(invoice, context);

    private static EitherAsync<IErrorMessage, CalculatedInvoice> AsCalculatedInvoice(IInvoice invoice)
        => invoice switch
        {
            CalculatedInvoice calculatedInvoice => calculatedInvoice,
            _ => new UnexpectedErrorMessage($"Expected {nameof(CalculatedInvoice)} but received {invoice.GetType().Name}")
        };
}
