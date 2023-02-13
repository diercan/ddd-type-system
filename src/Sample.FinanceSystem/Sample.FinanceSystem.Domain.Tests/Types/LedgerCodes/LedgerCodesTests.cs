using LanguageExt;
using Sample.FinanceSystem.Domain.Types.LedgerCodeTypes;

namespace Sample.FinanceSystem.Domain.Tests.Types.LedgerCodes
{
    public class LedgerCodesTests
    {
        [Theory]
        [InlineData("", "Empty code.", "Empty string is not a valid ledger code. Ledger codes are 6 digit codes.")]
        [InlineData("123", "Code to short.", "123 is not a valid ledger code. Ledger codes are 6 digit codes.")]
        [InlineData("1234567", "Code to long.", "1234567 is not a valid ledger code. Ledger codes are 6 digit codes.")]
        [InlineData("123ABC", "Code with letters.", "123ABC is not a valid ledger code. Ledger codes are 6 digit codes.")]
        // This is an invalid test.
        //[InlineData("712345", "Code with invalid type", "712345 is not a valid ledger type.")]
        public void TestInvalidCodes(string code, string? description, string expectedErrorMessage)
        {
            var invalidCode = LedgerCode.Parse(code, description);
            invalidCode.Match(
                Right: ledger => Assert.Fail($"Should fail for {description}, but got {ledger}."),
                Left: error => error.Message.Should().Be(expectedErrorMessage));
        }

        [Theory]
        [InlineData("100000", "Asset ledger code", typeof(AssetLedgerCode))]
        [InlineData("131100", "Office assets - desks", typeof(AssetLedgerCode))]
        [InlineData("131104", "Office assets - copier", typeof(AssetLedgerCode))]
        [InlineData("200000", "Liability ledger code", typeof(LiabilityLedgerCode))]
        [InlineData("230100", "Current liability", typeof(LiabilityLedgerCode))]
        [InlineData("250100", "Long term liability", typeof(LiabilityLedgerCode))]
        [InlineData("300000", "Fund balance ledger code", typeof(FundBalanceLedgerCode))]
        [InlineData("310100", "General fund", typeof(FundBalanceLedgerCode))]
        [InlineData("350100", "Special projects fund", typeof(FundBalanceLedgerCode))]
        [InlineData("400000", "Fund balance ledger code", typeof(RevenueLedgerCode))]
        [InlineData("410100", "Contributions", typeof(RevenueLedgerCode))]
        [InlineData("420100", "Donations", typeof(RevenueLedgerCode))]
        [InlineData("460100", "Sales revenue", typeof(RevenueLedgerCode))]
        [InlineData("500000", "Expese ledger code", typeof(ExpenseLedgerCode))]
        [InlineData("510100", "Salaries", typeof(ExpenseLedgerCode))]
        [InlineData("550100", "Supplies", typeof(ExpenseLedgerCode))]
        [InlineData("800000", "Transfer ledger code", typeof(TransfersLedgerCode))]
        [InlineData("810100", "Stock transfer year end", typeof(TransfersLedgerCode))]
        [InlineData("850100", "Recoverable charges", typeof(TransfersLedgerCode))]
        // This is an invalid test.
        //[InlineData("150100", "Recoverable charges", typeof(TransfersLedgerCode))] 
        public void TestValidCodes(string code, string description, Type ledgerType)
        {
            var ledgerCode = LedgerCode.Parse(code, description);
            ledgerCode.Match(
                Right: ledger =>
                {
                    ledger.Should().BeOfType(ledgerType);
                    ledger.Code.Should().Be(code);
                    ledger.Description.Should().Be(description);
                },
                Left: error => Assert.Fail($"Expected a valid code of type {ledgerType} for {code} - {description}"));
        }
    }
}
