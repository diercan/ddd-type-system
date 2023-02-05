namespace Sample.FinanceSystem.Domain.Types.LedgerCodes
{
    public record LedgerCode(LedgerType Type, string Code, string? Description)
    {
        private readonly LedgerType _type = Type;
        private readonly string _code = GetValidatedCode(Code, Type);
        private readonly string? _description = Description;

        public LedgerType LedgerType { get => _type; init => _type = value; }
        public string Code { get => _code; init => _code = GetValidatedCode(value, LedgerType); }
        public string? Description { get => _description; init => _description = value; }

        private static string GetValidatedCode(string code, LedgerType Type)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new InvalidLedgerCodeException("Empty string is not a valid ledger code. Ledger codes are 6 digit codes.");
            if (code.Length != 6)
                throw new InvalidLedgerCodeException($"{code} is not a valid ledger code. Ledger codes are 6 digit codes.");
            if (!uint.TryParse(code, out _))
                throw new InvalidLedgerCodeException($"{code} is not a valid ledger code. Ledger codes are 6 digit codes.");
            if (code[..1] != ((int)Type).ToString())
                throw new InvalidLedgerCodeException($"{code} is not part of the correct ledger type. Code should start with {(int)Type}");

            return code;
        }
    }

    //public record LedgerCode(LedgerType Type, string Code, string Description, string VatCode, string ReportCode, string FundCode);

    // Report Code is a static list - possibly the project code from Financials
    //100	Staff Costs
    //110	Maintenance of Premises
    //120	Other Occupancy Costs
    //130	Educational Supplies & Services
    //140	Other Supplies & Services
    //150	Furniture & Equipment
    //160	Technology Costs
    //190	Depreciation
    //400	GAG/LA Funding
    //410	Other Dept for Education Grants
    //420	Other Government Grants
    //430	Private Sector Sponsorship
    //60000	Fixed Assets
    //60010	Current Assets
    //65000	Stock
    //65010	Debtors
    //65012	Trade
    //65014	Taxation
    //65020	Bank
    //65030	Petty Cash
    //70000	Creditors
    //70010	Pension Scheme Liability
    //80000	Balance B/F
    //170	Staff Development
    //180	Other Costs
    //199	Other Expenditure
    //440	Other Income
}
