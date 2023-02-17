using Sample.FinanceSystem.Domain.Types.AddressTypes;

namespace Sample.FinanceSystem.Domain.Types.CustomerTypes;
public record Customer(
    Name Name,
    Code Code,
    VatRegistrationNumber VatRegistrationNumber,
    Address Address);

public record UnvalidatedCustomer(
    string? Name,
    string? Code,
    string? VatRegistrationNumber,
    UnvalidatedAddress Address);