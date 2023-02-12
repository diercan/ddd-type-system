namespace Sample.FinanceSystem.Domain.Types.PaymentTypes;

[AsChoice]
public static partial class Payment
{
    public interface IPayment
    {
        public DateOnly Date { get; init; }
        //TODO: replace decimal with Money
        public decimal Amount { get; init; }
    }

    public record CardPayment : IPayment
    {
        public DateOnly Date { get; init; }
        //TODO: replace decimal with Money
        public decimal Amount { get; init; }
        public CardLast4Digits CardLast4Digits { get; init; } = CardLast4Digits.Default;
        public CardType CardType { get; set; }
    }

    public record BankPayment : IPayment
    {
        public DateOnly Date { get; init; }
        //TODO: replace decimal with Money
        public decimal Amount { get; init; }
        public Iban SourceIban { get; set; } = Iban.Default;
        public Iban DestinationIban { get; set; } = Iban.Default;
    }
}
