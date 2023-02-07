namespace Sample.FinanceSystem.Domain.WhatsWrong
{
    public class PaymentDetails
    {
        //different payemnts methods can have different details
        public string PaymentMethod { get; set; }
        public decimal AmountPaid { get; set; }
        public string Reference { get; set; }
        public DateOnly Date { get; set; }
    }
}
