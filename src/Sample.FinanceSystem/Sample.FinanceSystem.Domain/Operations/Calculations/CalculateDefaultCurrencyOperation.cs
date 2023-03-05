using Sample.FinanceSystem.Domain.Operations.Common;
using Sample.FinanceSystem.Domain.Types;
using Sample.FinanceSystem.Domain.Types.MoneyTypes;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Domain.Operations.Calculations;

internal class CalculateDefaultCurrencyOperation : InvoiceOperation<UnvalidatedInvoice>
{
    public bool ValidateCurrency { get; init; } = false;

    public override IInvoice Run(UnvalidatedInvoice input, InvoiceContext context)
    {
        if (input.Currency != null)
            return input;

        if (Enum.TryParse(context.CustomerContext.CurrencyCode, true, out Currency currency))
            return input with { Currency = currency };

        if (!ValidateCurrency)
            return input;

        return new InvalidInvoice(input, new ValidationError(
            $"Failed to calculate the default invoice currency for customer with code {context.CustomerContext.Code}",
            nameof(UnvalidatedInvoice.Currency)));
    }
}
