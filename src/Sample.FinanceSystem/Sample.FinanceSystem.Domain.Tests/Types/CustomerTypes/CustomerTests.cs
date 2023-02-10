using LanguageExt;
using Sample.FinanceSystem.Domain.Types.AddressTypes;
using Sample.FinanceSystem.Domain.Types.Common;
using Sample.FinanceSystem.Domain.Types.CustomerTypes;

namespace Sample.FinanceSystem.Domain.Tests.Types.CustomerTypes;
public class CustomerTests
{
    [Fact]
    public void CustomerCreatedSuccessfully()
    {
        //arrange
        Either<ErrorMessage, Address> addressEither = CreateAddress();

        var codeEither = Code.Parse("CT500");
        var nameEither = Name.Parse("Access");
        var vatRegistrationNumberEither = VatRegistrationNumber.Parse("RO123456");

        //act
        var customerEither = from address in addressEither
                             from code in codeEither
                             from name in nameEither
                             from vatRegistrationNumber in vatRegistrationNumberEither
                             select new Customer(name, code, vatRegistrationNumber, address);

        //assert
        customerEither.Match(
            Left: errorMessage => Assert.Fail($"There should be no error, instead '{errorMessage}' was found."),
            Right: customer =>
            {
                Assert.Equal("Access", customer.Name.Value);
                Assert.Equal("CT500", customer.Code.Value);
                Assert.Equal("RO123456", customer.VatRegistrationNumber.Value);
                Assert.Equal("Timisoara", customer.Address.City.Value);
                Assert.Equal("300000", customer.Address.ZipCode.Value);
                Assert.Equal("Piata Unirii, 1", customer.Address.AddressLine1.Value);
                Assert.Null(customer.Address.AddressLine2);
            }
        );
    }

    [Fact]
    public void CustomerCreationFailForInvalidData()
    {
        //arrange
        Either<ErrorMessage, Address> addressEither = CreateAddress();

        var codeEither = Code.Parse("CT500");
        var nameEither = Name.Parse("Access");
        var vatRegistrationNumberEither = VatRegistrationNumber.Parse("R123456");

        //act
        var customerEither = from address in addressEither
                             from code in codeEither
                             from name in nameEither
                             from vatRegistrationNumber in vatRegistrationNumberEither
                             select new Customer(name, code, vatRegistrationNumber, address);

        //assert
        customerEither.Match(
            Left: errorMessage => Assert.False(string.IsNullOrEmpty(errorMessage.Message)),
            Right: customer => Assert.Fail("Should not be possible to create a customer with invalid data.")
        );
    }

    private static Either<ErrorMessage, Address> CreateAddress()
    {
        var cityEither = City.Parse("Timisoara");
        var zipCodeEither = ZipCode.Parse("300000");
        var addressLineEither = AddressLine.Parse("Piata Unirii, 1");
        var address = from city in cityEither
                      from zipCode in zipCodeEither
                      from addressLine in addressLineEither
                      select new Address(city, zipCode, addressLine);
        return address;
    }
}
