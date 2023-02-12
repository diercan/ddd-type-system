using Sample.FinanceSystem.Domain.Operations.Common;
using Sample.FinanceSystem.Domain.Types;
using Sample.FinanceSystem.Domain.Types.DetailLineTypes;
using Sample.FinanceSystem.Domain.Types.InvoiceDetailLineTypes;
using Sample.FinanceSystem.Domain.Types.InvoiceTypes;
using Sample.FinanceSystem.Domain.Types.MoneyTypes;
using Sample.FinanceSystem.Domain.Types.VatTypes;
using static Sample.FinanceSystem.Domain.Types.Invoice;

namespace Sample.FinanceSystem.Domain.Operations.Calculations;

internal class CalculateDetailLinesTotalOperation : InvoiceOperation<UnvalidatedInvoice>
{
    public override IInvoices Run(UnvalidatedInvoice input, InvoiceContext context) 
        => new CalculatedInvoice(
            input.Customer,
            input.CreationDate,
            // Todo: Due date should already be available from the CalculateDueDateOperation? Need more states for the invoice?
            DateOnly.FromDateTime(DateTime.Now.Date),
            // Todo: The invoice total can be made as a separate operation that sums up the detail lines?
            new InvoiceTotal(new Money(0, Currency.RON), new Money(0, Currency.RON), new Money(0, Currency.RON)),
            input.Lines.Select(CalculateDetailLine));

    private static DetailLine CalculateDetailLine(DetailLine invoiceLine)
    {
        Currency currency = invoiceLine.Total.Gross.Currency;
        return invoiceLine with
        {
            Total = invoiceLine.Total switch
            {
                { Gross.Value: not 0 } => CalculateByGrossEntry(invoiceLine.Total, invoiceLine.Vat.Percentage),
                { Net.Value: not 0 } => CalculateByNetEntry(invoiceLine.Total, invoiceLine.Vat.Percentage),
                _ => new DetailLineTotal(new Money(0, currency), new Money(0, currency), new Money(0, currency))
            }
        };
    }

    private static DetailLineTotal CalculateByGrossEntry(DetailLineTotal total, VatPercentage percentage)
    {
        Money taxValue = total.Gross - (total.Gross / (1 + percentage.Value / 100));
        return total with
        {
            Net = total.Gross - taxValue,
            Tax = taxValue
        };
    }

    private static DetailLineTotal CalculateByNetEntry(DetailLineTotal total, VatPercentage percentage)
    {
        Money taxValue = total.Net * (percentage.Value / 100);
        return total with
        {
            Tax = taxValue,
            Gross = total.Net + taxValue
        };
    }
}
