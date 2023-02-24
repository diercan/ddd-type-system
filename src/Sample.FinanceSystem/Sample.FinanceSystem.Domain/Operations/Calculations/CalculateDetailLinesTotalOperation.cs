using LanguageExt;
using Sample.FinanceSystem.Domain.Operations.Common;
using Sample.FinanceSystem.Domain.Types;
using Sample.FinanceSystem.Domain.Types.DetailLineTypes;
using Sample.FinanceSystem.Domain.Types.InvoiceDetailLineTypes;
using Sample.FinanceSystem.Domain.Types.VatTypes;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Domain.Operations.Calculations;

internal class CalculateDetailLinesTotalOperation : InvoiceOperation<UnvalidatedInvoice, UnvalidatedInvoice>
{
    public override EitherAsync<IErrorMessage, UnvalidatedInvoice> Run(UnvalidatedInvoice input, InvoiceContext context)
        => input with { Lines = input.Lines.Select(CalculateDetailLine).ToList().AsReadOnly() };

    private static UnvalidatedDetailLine CalculateDetailLine(UnvalidatedDetailLine invoiceLine)
        => invoiceLine with
        {
            Total = (invoiceLine.Total, invoiceLine.Vat) switch
            {
                ({ Gross: not null and not 0 }, { Percentage: not null }) => CalculateByGrossEntry(invoiceLine.Total, invoiceLine.Vat),
                ({ Net: not null and not 0 }, { Percentage: not null }) => CalculateByNetEntry(invoiceLine.Total, invoiceLine.Vat),
                _ => invoiceLine.Total
            }
        };

    private static UnvalidatedDetailLineTotal CalculateByGrossEntry(UnvalidatedDetailLineTotal total, UnvalidatedVat vat)
    {
        decimal? taxValue = total.Gross - (total.Gross / (1 + vat.Percentage / 100));
        return total with
        {
            Net = total.Gross - taxValue,
            Tax = taxValue
        };
    }

    private static UnvalidatedDetailLineTotal CalculateByNetEntry(UnvalidatedDetailLineTotal total, UnvalidatedVat vat)
    {
        decimal? taxValue = total.Net * (vat.Percentage / 100);
        return total with
        {
            Tax = taxValue,
            Gross = total.Net + taxValue
        };
    }
}
