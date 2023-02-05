namespace Sample.FinanceSystem.Domain.Types.Generic
{
    public class InvalidPercentageException : Exception
    {
        public InvalidPercentageException(string message)
            : base(message) { }
    }
}