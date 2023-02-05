namespace Sample.FinanceSystem.Domain.Types.LedgerCodes
{
    [AsChoice]
    public static partial class LedgerCodeChoices
    {
        public interface ILedgerCode { }

        public record InvalidLedgerCode(string Code, string ErrorMessage) : ILedgerCode;
        public record AssetLedgerCode(LedgerCode Inner) : ILedgerCode;
        public record LiabilityLedgerCode(LedgerCode Inner) : ILedgerCode;
        public record FundBalanceLedgerCode(LedgerCode Inner) : ILedgerCode;
        public record RevenueLedgerCode(LedgerCode Inner) : ILedgerCode;
        public record ExpenseLedgerCode(LedgerCode Inner) : ILedgerCode;
        public record TransfersLedgerCode(LedgerCode Inner) : ILedgerCode;

        public static ILedgerCode CreateLedgerCode(string code, string? description)
        {
            if (string.IsNullOrWhiteSpace(code))
                return new InvalidLedgerCode(code, "Empty string is not a valid ledger code. Ledger codes are 6 digit codes.");
            if (code.Length != 6)
                return new InvalidLedgerCode(code, $"{code} is not a valid ledger code. Ledger codes are 6 digit codes.");
            if (!uint.TryParse(code, out _))
                return new InvalidLedgerCode(code, $"{code} is not a valid ledger code. Ledger codes are 6 digit codes.");

            int ledgerType = int.Parse(code[..1]);
            if (!Enum.IsDefined(typeof(LedgerType), ledgerType))
                return new InvalidLedgerCode(code, $"{code} is not a valid ledger type.");

            return (LedgerType)ledgerType switch
            {
                LedgerType.Asset => new AssetLedgerCode(new LedgerCode(LedgerType.Asset, code, description)),
                LedgerType.Liability => new LiabilityLedgerCode(new LedgerCode(LedgerType.Liability, code, description)),
                LedgerType.FundBalance => new FundBalanceLedgerCode(new LedgerCode(LedgerType.FundBalance, code, description)),
                LedgerType.Revenue => new RevenueLedgerCode(new LedgerCode(LedgerType.Revenue, code, description)),
                LedgerType.Expense => new ExpenseLedgerCode(new LedgerCode(LedgerType.Expense, code, description)),
                LedgerType.Transfers => new TransfersLedgerCode(new LedgerCode(LedgerType.Transfers, code, description)),
                _ => new InvalidLedgerCode(code, $"{code} is not a valid ledger type."),
            };
        }
    }
}
