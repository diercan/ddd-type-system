using LanguageExt;
using Sample.FinanceSystem.Domain.Types.Common;
using Sample.FinanceSystem.Domain.Types.VatTypes;

namespace Sample.FinanceSystem.Domain.Tests.Types.VatTypes
{
    public class VatPercentageTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(20)]
        [InlineData(40)]
        public void CheckValidPercentages(decimal percentage)
        {
            Either<ErrorMessage, VatPercentage> vat = VatPercentage.Parse(percentage);
            vat.Match(
                Right: p => p.Value.Should().Be(percentage),
                Left: e => Assert.Fail($"Received {e.Message} for {percentage}."));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(41)]
        [InlineData(100)]
        public void CheckInvalidPercentages(decimal percentage)
        {
            Either<ErrorMessage, VatPercentage> vat = VatPercentage.Parse(percentage);
            vat.Match(
                Right: p => Assert.Fail($"Received a valid {nameof(VatPercentage)} - {p} for {percentage}"),
                Left: e => e.Message.Should().StartWith($"{percentage} is not a valid rate."));
        }
    }
}
