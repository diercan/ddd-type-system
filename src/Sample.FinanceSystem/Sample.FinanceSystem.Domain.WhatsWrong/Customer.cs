namespace Sample.FinanceSystem.Domain.WhatsWrong;
public class Customer
{
    public string Name { get; set; } //max 100 characters
    public string Code { get; set; } // 5 digits and letters
    public string VatRegistrationNumber { get; set; } //2 letters followed by 6 digits
    public Address Address { get; set; }
}
