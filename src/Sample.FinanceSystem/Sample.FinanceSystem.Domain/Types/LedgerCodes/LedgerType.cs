namespace Sample.FinanceSystem.Domain.Types.LedgerCodes
{
    //public record LedgerType(int Type, string Description);

    public enum LedgerType
    {
        Asset = 1,
        Liability = 2,
        FundBalance = 3,
        Revenue = 4,
        Expense = 5,
        Transfers = 8
    };
}
