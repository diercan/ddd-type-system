using Sample.FinanceSystem.Domain.Types.InvoiceDetailLineTypes;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;
using static Sample.FinanceSystem.Domain.Tests.Operations.Calculations.CalculateVatPercentageOperation.CalculateVatPercentageInputs;

namespace Sample.FinanceSystem.Domain.Tests.Operations.Calculations.CalculateVatPercentageOperation;

public static class CalculateVatPercentageExpectedValues
{
    public static readonly UnvalidatedDetailLine DetailLineVat5Percent = new("line2", new("234567", "code2"), new("5%", 5), new());

    //public static readonly UnvalidatedInvoice InvoiceWithVatFromInputFirstOfFeb = new(FirstOfFeb, null, NullCustomer, null, new(), new[] { DetailLineWithVat });
    public static readonly UnvalidatedInvoice InvoiceWith5PercentVatFirstOfFeb = new(FirstOfFeb, null, NullCustomer, null, new(), new[] { DetailLineVat5Percent });
    //public static readonly UnvalidatedInvoice InvoiceWithNoVatFirstOfJan = new(FirstOfJan, null, NullCustomer, null, new(), new[] { DetailLineNoVat });
}
