using Sample.FinanceSystem.Domain.Types.AddressTypes;

namespace Sample.FinanceSystem.Domain.Tests.Types.AddressTypes;
public class ZipCodeTests
{

    [Fact]
    public void ValidZipCodeCanBeParsed()
    {
        //arrange
        var zipCodeString = "300000";

        //act
        var zipCodeEither = ZipCode.Parse(zipCodeString);

        //assert
        zipCodeEither.Match(
            Left: errorMessage => Assert.Fail($"Expected valid zip code instead found '{errorMessage}'"),
            Right: zipCode => Assert.Equal("300000", zipCode.Value)
        );
    }

    [Theory]
    [InlineData("30000")]
    [InlineData("")]
    [InlineData("3000000")]
    public void InvalidZipCodeFailsToParse(string zipCodeString)
    {
        //arrange

        //act
        var zipCodeEither = ZipCode.Parse(zipCodeString);

        //assert
        zipCodeEither.Match(
            Left: errorMessage => Assert.False(string.IsNullOrWhiteSpace(errorMessage.Message)),
            Right: zipCode => Assert.Fail("Invlaid zip code should not be parse successfully")
        );
    }
}
