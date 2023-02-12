using Sample.FinanceSystem.Domain.Types.VatTypes;

namespace Sample.FinanceSystem.Domain.Tests.Types.VatTypes
{
    public class VatTests
    {
        [Theory]
        [InlineData("S20%", 20)]
        [InlineData("R5%", 5)]
        [InlineData("Z0%", 0)]
        public void VatCreatedSuccessfully(string code, decimal percentage)
        {
            var vat = from vatCode in VatCode.Parse(code)
                      from vatPercentage in VatPercentage.Parse(percentage)
                      select new Vat(vatCode, vatPercentage);

            vat.Match(
                Right: vat =>
                {
                    vat.Code.Value.Should().Be(code);
                    vat.Percentage.Value.Should().Be(percentage);
                },
                Left: error => Assert.Fail($"Expected valid {nameof(Vat)} but received {error.Message}"));
        }

        [Theory]
        [InlineData("VeryLongCode", 20, "Value VeryLongCode is an invalid VatCode")]
        [InlineData("A", 20, "Value A is an invalid VatCode")]
        [InlineData("S20%", 50, "50 is not a valid rate. VAT rate cannot exceed 40%.")]
        [InlineData("S20%", -1, "-1 is not a valid rate. VAT rate cannot be negative.")]
        public void VatFailToCreateIfAnyPartFailed(string code, decimal percentage, string expectedError)
        {
            var vat = from vatCode in VatCode.Parse(code)
                      from vatPercentage in VatPercentage.Parse(percentage)
                      select new Vat(vatCode, vatPercentage);

            vat.Match(
                Right: vat => Assert.Fail($"Expected {expectedError} but received {vat}"),
                Left: error => error.Message.Should().Be(expectedError));
        }
    }
}
