namespace Sample.FinanceSystem.Domain.Types.Vat
{
    public class InvalidVatCodeException : Exception
    {
        public InvalidVatCodeException(string message)
            : base(message) { }
    }
}
