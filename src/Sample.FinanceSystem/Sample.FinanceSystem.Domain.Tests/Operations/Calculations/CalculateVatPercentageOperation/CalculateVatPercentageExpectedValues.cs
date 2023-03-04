using Sample.FinanceSystem.Domain.Types.DetailLineTypes;
using Sample.FinanceSystem.Domain.Types.InvoiceDetailLineTypes;
using static Sample.FinanceSystem.Domain.Tests.Operations.Calculations.CalculateVatPercentageOperation.CalculateVatPercentageInputs;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Domain.Tests.Operations.Calculations.CalculateVatPercentageOperation;

public static class CalculateVatPercentageExpectedValues
{
    public static readonly UnvalidatedDetailLine DetailLineVat5Percent = new("line2", new("234567", "code2"), new("5%", 5), new());

    public static readonly UnvalidatedInvoice ExpectedInvoiceWithVat5PerCodeFirstOfJan = new(FirstOfJan, null, NullCustomer, null, new(), new[] { DetailLineVat5Percent }.AsDetailLines());
    public static readonly UnvalidatedInvoice ExpectedInvoiceWithVat5PerCodeFirstOfFeb = new(FirstOfFeb, null, NullCustomer, null, new(), new[] { DetailLineVat5Percent }.AsDetailLines());
}
