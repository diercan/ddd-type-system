namespace Sample.FinanceSystem.Domain.Types.Vat
{
    public class InvalidVatRateException : Exception
    {
        public InvalidVatRateException(string message)
            : base(message) { }

        public InvalidVatRateException(decimal invalidRate)
            : base($"{invalidRate} is not a valid VAT rate") { }
    }
}
