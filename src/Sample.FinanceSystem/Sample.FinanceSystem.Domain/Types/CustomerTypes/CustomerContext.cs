namespace Sample.FinanceSystem.Domain.Types.CustomerTypes;

public record CustomerContext(
    int Id,
    string Name,
    string Code,
    string VatRegistrationNumber,
    string CurrencyCode,
    string City,
    string ZipCode,
    string AddressLine1,
    string? AddressLine2);
