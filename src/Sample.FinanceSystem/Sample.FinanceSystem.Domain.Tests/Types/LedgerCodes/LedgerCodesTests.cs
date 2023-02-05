using LanguageExt;
using Sample.FinanceSystem.Domain.Types.LedgerCodes;
using static Sample.FinanceSystem.Domain.Types.LedgerCodes.LedgerCodeChoices;

namespace Sample.FinanceSystem.Domain.Tests.Types.LedgerCodes
{
    public class LedgerCodesTests
    {
        [Theory]
        [InlineData("", "Empty code.", "Empty string is not a valid ledger code. Ledger codes are 6 digit codes.")]
        [InlineData("123", "Code to short.", "123 is not a valid ledger code. Ledger codes are 6 digit codes.")]
        [InlineData("1234567", "Code to long.", "1234567 is not a valid ledger code. Ledger codes are 6 digit codes.")]
        [InlineData("123ABC", "Code with letters.", "123ABC is not a valid ledger code. Ledger codes are 6 digit codes.")]
        [InlineData("712345", "Code with invalid type", "712345 is not a valid ledger type.")]
        public void TestInvalidCodes(string code, string? description, string expectedErrorMessage)
        {
            var invalidCode = CreateLedgerCode(code, description);
            invalidCode.Match<ILedgerCode>(
                whenInvalidLedgerCode: invalidCode => invalidCode.Test(c => c.ErrorMessage.Should().Be(expectedErrorMessage)),
                whenAssetLedgerCode: validCode => validCode.AssertFail($"Should fail for {description}, but got {validCode}."),
                whenLiabilityLedgerCode: validCode => validCode.AssertFail($"Should fail for {description}, but got {validCode}."),
                whenFundBalanceLedgerCode: validCode => validCode.AssertFail($"Should fail for {description}, but got {validCode}."),
                whenRevenueLedgerCode: validCode => validCode.AssertFail($"Should fail for {description}, but got {validCode}."),
                whenExpenseLedgerCode: validCode => validCode.AssertFail($"Should fail for {description}, but got {validCode}."),
                whenTransfersLedgerCode: validCode => validCode.AssertFail($"Should fail for {description}, but got {validCode}."));
        }

        [Theory]
        [InlineData("100000", "Asset ledger code", LedgerType.Asset)]
        [InlineData("131100", "Office assets - desks", LedgerType.Asset)]
        [InlineData("131104", "Office assets - copier", LedgerType.Asset)]
        [InlineData("200000", "Liability ledger code", LedgerType.Liability)]
        [InlineData("230100", "Current liability", LedgerType.Liability)]
        [InlineData("250100", "Long term liability", LedgerType.Liability)]
        [InlineData("300000", "Fund balance ledger code", LedgerType.FundBalance)]
        [InlineData("310100", "General fund", LedgerType.FundBalance)]
        [InlineData("350100", "Special projects fund", LedgerType.FundBalance)]
        [InlineData("400000", "Fund balance ledger code", LedgerType.Revenue)]
        [InlineData("410100", "Contributions", LedgerType.Revenue)]
        [InlineData("420100", "Donations", LedgerType.Revenue)]
        [InlineData("460100", "Sales revenue", LedgerType.Revenue)]
        [InlineData("500000", "Expese ledger code", LedgerType.Expense)]
        [InlineData("510100", "Salaries", LedgerType.Expense)]
        [InlineData("550100", "Supplies", LedgerType.Expense)]
        [InlineData("800000", "Transfer ledger code", LedgerType.Transfers)]
        [InlineData("810100", "Stock transfer year end", LedgerType.Transfers)]
        [InlineData("850100", "Recoverable charges", LedgerType.Transfers)]
        //[InlineData("150100", "Recoverable charges", LedgerType.Transfers)]
        public void TestValidCodes(string code, string description, LedgerType ledgerType)
        {
            var ledgerCode = CreateLedgerCode(code, description);
            ledgerCode.Match<Unit>(
                whenInvalidLedgerCode: invalidCode => Fail($"Expected a valid code of type {ledgerType} for {code} - {description}"),
                whenAssetLedgerCode: assetCode => AssertLedgerCode(assetCode.Inner, code, description, ledgerType), 
                whenLiabilityLedgerCode: liabilityCode => AssertLedgerCode(liabilityCode.Inner, code, description, ledgerType), 
                whenFundBalanceLedgerCode: fundCode => AssertLedgerCode(fundCode.Inner, code, description, ledgerType),
                whenRevenueLedgerCode: revenueCode => AssertLedgerCode(revenueCode.Inner, code, description, ledgerType), 
                whenExpenseLedgerCode: expenseCode => AssertLedgerCode(expenseCode.Inner, code, description, ledgerType), 
                whenTransfersLedgerCode: transferCode => AssertLedgerCode(transferCode.Inner, code, description, ledgerType));
        }

        private static Unit Fail(string message)
        {
            Assert.Fail(message);
            return Unit.Default;
        }

        private static Unit AssertLedgerCode(LedgerCode ledgerCode, string code, string description, LedgerType type)
        {
            ledgerCode.Code.Should().Be(code);
            ledgerCode.LedgerType.Should().Be(type);
            ledgerCode.Description.Should().Be(description);

            return Unit.Default;
        }
    }
}
