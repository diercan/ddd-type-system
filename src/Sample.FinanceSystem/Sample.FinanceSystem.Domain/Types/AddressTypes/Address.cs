namespace Sample.FinanceSystem.Domain.Types.AddressTypes;

public record Address(
    City City,
    ZipCode ZipCode,
    AddressLine AddressLine1,
    AddressLine? AddressLine2 = null);

public record UnvalidatedAddress(
    string? City = null,
    string? ZipCode = null,
    string? AddressLine1 = null,
    string? AddressLine2 = null);