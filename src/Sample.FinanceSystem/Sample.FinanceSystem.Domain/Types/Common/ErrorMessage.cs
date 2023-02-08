namespace Sample.FinanceSystem.Domain.Types.Common
{
    public record ErrorMessage(string Message)
    {
        public override string ToString() => "Message";
    }
}
