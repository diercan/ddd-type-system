namespace Sample.FinanceSystem.Domain.WhatsWrong;

public class Address
{
    public string City { get; set; }    // max 50 characters
    public string AddressLine1 { get; set; } // max 200 characters
    public string AddressLine2 { get; set; } //field is optional
    public string ZipCode { get; set; } //6 digits number
}
