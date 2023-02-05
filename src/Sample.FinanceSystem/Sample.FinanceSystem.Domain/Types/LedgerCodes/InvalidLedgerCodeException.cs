namespace Sample.FinanceSystem.Domain.Types.LedgerCodes
{
    public class InvalidLedgerCodeException : Exception
    {
        public InvalidLedgerCodeException(string message)
        : base(message) { }
    }
}
