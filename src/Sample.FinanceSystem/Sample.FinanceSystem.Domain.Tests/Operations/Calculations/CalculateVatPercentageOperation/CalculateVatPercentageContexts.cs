using Sample.FinanceSystem.Domain.Types;
using Sample.FinanceSystem.Domain.Types.CustomerTypes;
using Sample.FinanceSystem.Domain.Types.VatTypes;

namespace Sample.FinanceSystem.Domain.Tests.Operations.Calculations.CalculateVatPercentageOperation;

public static class CalculateVatPercentageContexts
{
	public static readonly CustomerContext EmptyCustomerContext = new(0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, null);

	public static readonly VatContext VatStartingFirstOfFeb = new(1, "20%", 20, new DateOnly(2023, 2, 1));
	public static readonly VatContext VatStartingFirstOfJan = new(2, "5%", 5, new DateOnly(2023, 1, 1));
	public static readonly IReadOnlyList<VatContext> EmptyVatContext = new List<VatContext>().AsReadOnly();

	public static readonly InvoiceContext ContextWithoutVat = new(EmptyCustomerContext, EmptyVatContext);
	public static readonly InvoiceContext ContextWithVatStartingFirstOfFeb = new(EmptyCustomerContext, new[] { VatStartingFirstOfFeb });
	public static readonly InvoiceContext ContextWithVatStartingFirstOfJan = new(EmptyCustomerContext, new[] { VatStartingFirstOfJan });
	public static readonly InvoiceContext ContextWithMultipleVatPercentages = new(EmptyCustomerContext, new[] { VatStartingFirstOfJan, VatStartingFirstOfFeb });
}
