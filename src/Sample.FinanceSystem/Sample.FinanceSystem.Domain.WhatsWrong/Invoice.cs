namespace Sample.FinanceSystem.Domain.WhatsWrong
{
    public class Invoice
    {
        //Which values are optional
        //properties can be mutated since pass as reference

        public InvoiceStatus Status { get; set; }

        public Customer Customer { get; set; }

        //CreatioDate < DueDate
        public DateOnly CreationDate { get; set; }
        public DateOnly DueDate { get; set; }

        //PaymentDate only valid when invoice is paid
        public DateOnly PaymentDate { get; set; }
        //collection is mutable, PaymentDate is the payment that completes the invoice
        public List<PaymentDetails> Payments { get; set; }

        public List<DetailLine> Details { get; set; } //collection is mutable

        //fields that need to align to Details
        public decimal TotalGross { get; set; }
        public decimal TotalNet { get; set; }
        public decimal TotalTax { get; set; }
    }
}