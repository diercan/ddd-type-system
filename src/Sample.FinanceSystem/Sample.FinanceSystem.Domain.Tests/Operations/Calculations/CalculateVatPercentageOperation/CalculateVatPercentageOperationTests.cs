using static Sample.FinanceSystem.Domain.Tests.Operations.Calculations.CalculateVatPercentageOperation.CalculateVatPercentageContexts;
using static Sample.FinanceSystem.Domain.Tests.Operations.Calculations.CalculateVatPercentageOperation.CalculateVatPercentageInputs;
using static Sample.FinanceSystem.Domain.Tests.Operations.Calculations.CalculateVatPercentageOperation.CalculateVatPercentageExpectedValues;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;
using Sample.FinanceSystem.Domain.Types;
using LanguageExt;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;

namespace Sample.FinanceSystem.Domain.Tests.Operations.Calculations.CalculateVatPercentageOperation.CalculateVatPercentageOperation;

public class CalculateVatPercentageOperationTests
{
    public static IEnumerable<object[]> TestCases => new[]
    {
        new object[] { "Check that detail lines with VAT are not changed", InvoiceWithVatFirstOfFeb, ContextWithoutVat, InvoiceWithVatFirstOfFeb },
        new object[] { "Check that detail lines without VAT are not changed if VAT code does not exists in context", InvoiceNoVatFirstOfJan, ContextWithoutVat, InvoiceNoVatFirstOfJan }
    };

    [Theory]
    [MemberData(nameof(TestCases))]
    public async Task TestCalculateVatPercentageOperation(string description, UnvalidatedInvoice input, InvoiceContext context, UnvalidatedInvoice expected)
    {
        Domain.Operations.Calculations.CalculateVatPercentageOperation operation = new();
        EitherAsync<IErrorMessage, UnvalidatedInvoice> result = operation.Run(input, context);

        await result.Match(
            Right: actual => actual.Should().Be(expected, "received {actual} but expected {expected}", actual, expected),
            Left: error => Assert.Fail($"{description} failed with {error}"));
    }
}
