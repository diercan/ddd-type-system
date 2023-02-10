namespace Sample.FinanceSystem.Domain.Types.AddressTypes;
public record Address(
    City City,
    ZipCode ZipCode,
    AddressLine AddressLine1,
    AddressLine? AddressLine2
)
{
    public Address(
        City City,
        ZipCode ZipCode,
        AddressLine AddressLine1
    ) : this(City, ZipCode, AddressLine1, null) { }
}
