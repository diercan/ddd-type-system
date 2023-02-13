using Sample.FinanceSystem.Domain.Types.MoneyTypes;

namespace Sample.FinanceSystem.Domain.Tests.Types.MoneyTypes
{
    public class MoneyTests
    {
        [Fact]
        public void OperatorsThrowForDifferentCurrency()
        {
            Money euro = new(100, Currency.EUR);
            Money gbp = new(100, Currency.GBP);

            Assert.Throws<ArithmeticException>(() => euro <= gbp);
            Assert.Throws<ArithmeticException>(() => euro >= gbp);
            Assert.Throws<ArithmeticException>(() => euro < gbp);
            Assert.Throws<ArithmeticException>(() => euro > gbp);
            Assert.Throws<ArithmeticException>(() => euro + gbp);
            Assert.Throws<ArithmeticException>(() => euro - gbp);

            (euro != gbp).Should().BeTrue();
            (euro == gbp).Should().BeFalse();
        }

        [Fact]
        public void OperatorsReturnForSameCurrency()
        {
            Money amount1 = new(100, Currency.RON);
            Money amount2 = new(100, Currency.RON);
            Money amount3 = new(200, Currency.RON);

            (amount1 == amount2).Should().BeTrue();
            (amount1 != amount3).Should().BeTrue();
            (amount1 <= amount2).Should().BeTrue();
            (amount1 <= amount3).Should().BeTrue();
            (amount3 <= amount1).Should().BeFalse();
            (amount1 >= amount2).Should().BeTrue();
            (amount1 >= amount3).Should().BeFalse();
            (amount3 >= amount1).Should().BeTrue();
            (amount1 + amount2).Should().Be(new Money(amount1.Value + amount2.Value, Currency.RON));
            (amount1 - amount2).Should().Be(new Money(amount1.Value - amount2.Value, Currency.RON));
            (amount1 * 2).Should().Be(new Money(amount1.Value * 2, Currency.RON));
            (amount1 / 2).Should().Be(new Money(amount1.Value / 2, Currency.RON));
        }

        [Theory]
        [InlineData(100, Currency.EUR, "100,00 €")]
        [InlineData(100, Currency.USD, "$100.00")]
        [InlineData(100, Currency.RON, "100,00 RON")]
        [InlineData(100, Currency.GBP, "£100.00")]
        public void ToStringFormatsWithCurrency(decimal value, Currency currency, string formatted)
        {
            Money money = new(value, currency);
            money.ToString().Should().Be(formatted);
        }
    }
}
