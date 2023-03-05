using LanguageExt;
using Sample.FinanceSystem.Domain.Operations.Common;
using Sample.FinanceSystem.Domain.Types;
using Sample.FinanceSystem.Domain.Types.InvoiceDetailLineTypes;
using Sample.FinanceSystem.Domain.Types.VatTypes;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Domain.Operations.Calculations;

internal class CalculateVatPercentageOperation : InvoiceOperation<UnvalidatedInvoice>
{
    public bool ValidateVatPercentage { get; init; } = false;

    public override IInvoice Run(UnvalidatedInvoice input, InvoiceContext context)
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
                    .Select(v => v.Percentage)
                    .FirstOrDefault();

                return l with { Vat = l.Vat with { Percentage = vatPercentage } };
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
                return new InvalidInvoice(input,
                    new ValidationError($"Invalid VAT codes: {invalidVatCodes.Aggregate("", (err, c) => $"{err}, {c.Code}")}"));
        }

        return input with { Lines = linesWithVat };
    }
}
