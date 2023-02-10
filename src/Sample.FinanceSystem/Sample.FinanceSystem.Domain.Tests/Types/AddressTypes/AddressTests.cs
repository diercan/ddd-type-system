using Sample.FinanceSystem.Domain.Types.AddressTypes;

namespace Sample.FinanceSystem.Domain.Tests.Types.AddressTypes
{
    public class AddressTests
    {
        [Fact]
        public void AddressCreatedSuccessfully()
        {
            //arrange
            var cityEither = City.Parse("Timisoara");
            var zipCodeEither = ZipCode.Parse("300000");
            var addressLineEither = AddressLine.Parse("Piata Unirii, 1");

            //act
            var address = from city in cityEither
                          from zipCode in zipCodeEither
                          from addressLine in addressLineEither
                          select new Address(city, zipCode, addressLine);

            //assert
            address.Match(
                Left: errorMessage => Assert.Fail($"There should be no error, instead '{errorMessage}' was found."),
                Right: address =>
                {
                    Assert.Equal("Timisoara", address.City.Value);
                    Assert.Equal("300000", address.ZipCode.Value);
                    Assert.Equal("Piata Unirii, 1", address.AddressLine1.Value);
                    Assert.Null(address.AddressLine2);
                }
            );
        }

        [Fact]
        public void AddressFailToCreateIfAnyOfThePartsIsInvalid()
        {
            //arrange
            var cityEither = City.Parse("Timisoara");
            var zipCodeEither = ZipCode.Parse("300"); // invalid zip code
            var addressLineEither = AddressLine.Parse("Piata Unirii, 1");

            //act
            var address = from city in cityEither
                          from zipCode in zipCodeEither
                          from addressLine in addressLineEither
                          select new Address(city, zipCode, addressLine);

            //assert
            address.Match(
                Left: errorMessage => Assert.False(string.IsNullOrEmpty(errorMessage.Message)),
                Right: address => Assert.Fail($"Address should fail to create")
            );
        }
    }
}
