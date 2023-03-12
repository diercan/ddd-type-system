namespace Sample.FinanceSystem.Domain.WhatsWrong;

public record InvoiceWithFlags
{
    //...

    public bool IsValid { get; set; }
    public decimal Total { get; set; }

    public bool IsApproved { get; set; }
    public string ApproverName { get; set; }

    public bool IsPaid { get; set; }
    public PaymentDetails PaymentDetails { get; set; }

    //...
}
