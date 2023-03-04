using Sample.FinanceSystem.Domain.Types;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;
using static Sample.FinanceSystem.Domain.Tests.Operations.Calculations.CalculateVatPercentageOperation.CalculateVatPercentageContexts;
using static Sample.FinanceSystem.Domain.Tests.Operations.Calculations.CalculateVatPercentageOperation.CalculateVatPercentageInputs;
using static Sample.FinanceSystem.Domain.Tests.Operations.Calculations.CalculateVatPercentageOperation.CalculateVatPercentageExpectedValues;

namespace Sample.FinanceSystem.Domain.Tests.Operations.Calculations.CalculateVatPercentageOperation;

public static class CalculateVatPercentageOperationTestDataProvider
{
    public record TestData(UnvalidatedInvoice Input, InvoiceContext Context, UnvalidatedInvoice Expected);

    public static IEnumerable<object[]> UseCases()
    {
        yield return new object[]
        {
            "Lines with VAT",
            new MemberDataSerializer<TestData>(
                "Check that detail lines with VAT are not changed",
            new TestData(InvoiceWithVat5PerCodeFirstOfFeb, ContextWithoutVat, InvoiceWithVat5PerCodeFirstOfFeb))
        };
        yield return new object[] {
            "Lines without VAT",
            new MemberDataSerializer<TestData>(
                "Check that detail lines without VAT are not changed if VAT code does not exists in context",
                new TestData(InvoiceNoVat5PerCodeFirstOfJan, ContextWithoutVat, InvoiceNoVat5PerCodeFirstOfJan))
        };
        yield return new object[]
        {
            "Lines without VAT",
            new MemberDataSerializer<TestData>(
                "Check that detail lines without VAT are not changed if VAT code has a start date in future",
                new TestData(InvoiceNoVat5PerCodeFirstOfJan, ContextWithVat5PerCodeStartingFirstOfFeb, InvoiceNoVat5PerCodeFirstOfJan))
        };
        yield return new object[]
        {
            "Lines without VAT",
            new MemberDataSerializer<TestData>(
                "Check that detail lines without VAT are updated to include VAT from context",
                new TestData(InvoiceNoVat5PerCodeFirstOfFeb, ContextWithVat5PerCodeStartingFirstOfJan, ExpectedInvoiceWithVat5PerCodeFirstOfFeb))
        };
        yield return new object[]
        {
            "Lines without VAT",
            new MemberDataSerializer<TestData>(
                "Check that detail lines without VAT are updated with correcvt VAT percentage when context has multiple percentages",
                new TestData(InvoiceNoVat5PerCodeFirstOfFeb, ContextWithMultipleVatPercentages, ExpectedInvoiceWithVat5PerCodeFirstOfFeb))
        };
    }
}
