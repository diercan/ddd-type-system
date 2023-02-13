using LanguageExt;
using Sample.FinanceSystem.Domain.Types.Common;

namespace Sample.FinanceSystem.Domain.Types.LedgerCodeTypes
{
    public record AssetLedgerCode : LedgerCode;
    public record LiabilityLedgerCode : LedgerCode;
    public record FundBalanceLedgerCode : LedgerCode;
    public record RevenueLedgerCode : LedgerCode;
    public record ExpenseLedgerCode : LedgerCode;
    public record TransfersLedgerCode : LedgerCode;
    public record CustomLedgerCode : LedgerCode;

    public abstract record LedgerCode
    {
        public string Code { get; private set; } = default!;
        public string? Description { get; private set; }

        public override string ToString() => !string.IsNullOrEmpty(Description)
            ? $"{Code} - {Description}"
            : Code;

        public static Either<ErrorMessage, LedgerCode> Parse(string code, string? description)
        {
            if (string.IsNullOrWhiteSpace(code))
                return new ErrorMessage("Empty string is not a valid ledger code. Ledger codes are 6 digit codes.");
            if (code.Length != 6)
                return new ErrorMessage($"{code} is not a valid ledger code. Ledger codes are 6 digit codes.");
            if (!uint.TryParse(code, out _))
                return new ErrorMessage($"{code} is not a valid ledger code. Ledger codes are 6 digit codes.");

            int ledgerType = int.Parse(code[..1]);
            return ledgerType switch
            {
                1 => new AssetLedgerCode { Code = code, Description = description },
                2 => new LiabilityLedgerCode { Code = code, Description = description },
                3 => new FundBalanceLedgerCode { Code = code, Description = description },
                4 => new RevenueLedgerCode { Code = code, Description = description },
                5 => new ExpenseLedgerCode { Code = code, Description = description },
                8 => new TransfersLedgerCode { Code = code, Description = description },
                _ => new CustomLedgerCode { Code = code, Description = description },
            };
        }
    }
}
