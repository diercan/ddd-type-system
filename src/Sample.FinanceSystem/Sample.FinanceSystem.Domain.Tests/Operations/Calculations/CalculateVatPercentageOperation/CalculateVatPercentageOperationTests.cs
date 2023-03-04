using LanguageExt;
using static Sample.FinanceSystem.Domain.Tests.Operations.Calculations.CalculateVatPercentageOperation.CalculateVatPercentageOperationTestDataProvider;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Domain.Tests.Operations.Calculations.CalculateVatPercentageOperation.CalculateVatPercentageOperation;

public class CalculateVatPercentageOperationTests
{
    [Theory]
    [MemberData(nameof(CalculateVatPercentageOperationTestDataProvider.UseCases), MemberType = typeof(CalculateVatPercentageOperationTestDataProvider))]
    public async Task TestCalculateVatPercentageOperation(string description, MemberDataSerializer<TestData> testCase)
    {
        Domain.Operations.Calculations.CalculateVatPercentageOperation operation = new();
        EitherAsync<IErrorMessage, UnvalidatedInvoice> result = operation.Run(testCase.Object.Input, testCase.Object.Context);

        await result.Match(
            Right: actual => actual.Should().Be(testCase.Object.Expected),
            Left: error => Assert.Fail($"{description} failed with {error}"));
    }
}
