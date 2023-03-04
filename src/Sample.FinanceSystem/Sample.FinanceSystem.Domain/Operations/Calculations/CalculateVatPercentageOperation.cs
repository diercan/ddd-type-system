using LanguageExt;
using Sample.FinanceSystem.Domain.Operations.Common;
using Sample.FinanceSystem.Domain.Types;
using Sample.FinanceSystem.Domain.Types.DetailLineTypes;
using Sample.FinanceSystem.Domain.Types.InvoiceDetailLineTypes;
using Sample.FinanceSystem.Domain.Types.VatTypes;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Domain.Operations.Calculations;

public class CalculateVatPercentageOperation : InvoiceOperation<UnvalidatedInvoice, UnvalidatedInvoice>
{
    public bool ValidateVatPercentage { get; init; } = false;

    public override EitherAsync<IErrorMessage, UnvalidatedInvoice> Run(UnvalidatedInvoice input, InvoiceContext context)
    {
        if (input.Lines.All(l => l.Vat.Percentage.HasValue))
            return input;

        IReadOnlyList<UnvalidatedDetailLine> linesWithVat = input
            .Lines
            .Select(l =>
            {
                if (l.Vat.Percentage.HasValue)
                    return l;

                decimal? vatPercentage = context.VatContext
                    .Where(v => v.StarDate <= input.CreationDate && v.Code == l.Vat.Code)
                    .OrderBy(v => v.StarDate)
                    .FirstOrDefault()?.Percentage;

                return vatPercentage.HasValue
                    ? l with { Vat = l.Vat with { Percentage = vatPercentage } }
                    : l;
            })
            .ToList()
            .AsReadOnly();

        if (ValidateVatPercentage)
        {
            IEnumerable<UnvalidatedVat> invalidVatCodes = linesWithVat
                .Where(l => l.Vat.Percentage == null)
                .Select(l => l.Vat)
                .ToList();

            if (invalidVatCodes.Any())
                return new ValidationError($"Invalid VAT codes: {invalidVatCodes.Aggregate("", (err, c) => $"{err}, {c.Code}")}");
        }

        return input with { Lines = linesWithVat.AsDetailLines() };
    }
}
