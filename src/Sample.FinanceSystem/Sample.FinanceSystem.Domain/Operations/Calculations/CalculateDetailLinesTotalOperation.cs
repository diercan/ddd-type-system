using Sample.FinanceSystem.Domain.Operations.Common;
using Sample.FinanceSystem.Domain.Types;
using Sample.FinanceSystem.Domain.Types.DetailLineTypes;
using Sample.FinanceSystem.Domain.Types.InvoiceDetailLineTypes;
using Sample.FinanceSystem.Domain.Types.MoneyTypes;
using Sample.FinanceSystem.Domain.Types.VatTypes;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Domain.Operations.Calculations;

internal class CalculateDetailLinesTotalOperation : InvoiceOperation<UnvalidatedInvoice>
{
    public override IInvoice Run(UnvalidatedInvoice input, InvoiceContext context)
    {
        return input switch
        {
            { Currency: not null } => new UnvalidatedInvoice(
                input.Customer,
                input.CreationDate,
                input.Lines.Select(l => CalculateDetailLine(l, input.Currency.Value))),
            _ => new InvalidInvoice(
                input.Customer,
                input.CreationDate,
                new[] { new ValidationError("Currency is not set", nameof(UnvalidatedInvoice.Currency)) },
                input.Lines)
        };
    }

    private static DetailLine CalculateDetailLine(DetailLine invoiceLine, Currency currency)
        => invoiceLine with
        {
            Total = invoiceLine.Total switch
            {
                { Gross.Value: not 0 } => CalculateByGrossEntry(invoiceLine.Total, invoiceLine.Vat.Percentage),
                { Net.Value: not 0 } => CalculateByNetEntry(invoiceLine.Total, invoiceLine.Vat.Percentage),
                _ => new DetailLineTotal(new Money(0, currency), new Money(0, currency), new Money(0, currency))
            }
        };

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
