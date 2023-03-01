using Sample.FinanceSystem.Domain.Types.CustomerTypes;
using Sample.FinanceSystem.Domain.Types.InvoiceDetailLineTypes;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Domain.Tests.Operations.Calculations.CalculateVatPercentageOperation;

public static class CalculateVatPercentageInputs
{
    public static readonly DateOnly FirstOfJan = new(2023, 1, 1);
    public static readonly DateOnly FirstOfFeb = new(2023, 2, 1);

    public static readonly UnvalidatedCustomer NullCustomer = new(null, null, null, new());

    public static readonly UnvalidatedDetailLine DetailLineWithVat = new("line1", new("123456", "code1"), new("5%", 20), new());
    public static readonly UnvalidatedDetailLine DetailLineNoVat = new("line2", new("234567", "code2"), new("5%"), new());

    public static readonly UnvalidatedInvoice InvoiceWithVatFirstOfFeb = new(FirstOfFeb, null, NullCustomer, null, new(), new[] { DetailLineWithVat });
    public static readonly UnvalidatedInvoice InvoiceNoVatFirstOfFeb = new(FirstOfFeb, null, NullCustomer, null, new(), new[] { DetailLineNoVat });
    public static readonly UnvalidatedInvoice InvoiceNoVatFirstOfJan = new(FirstOfJan, null, NullCustomer, null, new(), new[] { DetailLineNoVat });

}
