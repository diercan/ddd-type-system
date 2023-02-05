using Sample.FinanceSystem.Domain.Types.Vat;
using Sample.FinanceSystem.Domain.Types.Vat.WithTypes;

namespace Sample.FinanceSystem.Domain.Tests.Types.Vat.WithTypes
{
    public class VatRateTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(20)]
        [InlineData(40)]
        public void CheckValidRates(decimal rate)
        {
            VatRate vatRate = new(rate);
            Assert.Equal(rate, vatRate.Value);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(41)]
        [InlineData(100)]
        public void CheckInvalidRates(decimal rate)
        {
            Assert.Throws<InvalidVatRateException>(() => new VatRate(rate));
        }

        [Fact]
        public void CannotCreateInvalidStateUsingWithExpression()
        {
            VatRate vatRate = new(20);
            Assert.Throws<InvalidVatRateException>(() => vatRate with { Value = 50 });
        }
    }
}
