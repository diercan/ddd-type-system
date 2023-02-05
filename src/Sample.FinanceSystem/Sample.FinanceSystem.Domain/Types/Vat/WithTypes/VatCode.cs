namespace Sample.FinanceSystem.Domain.Types.Vat.WithTypes
{
    public record VatCode(string Code, string Description)
    {
        private readonly string _code = GetValidatedCode(Code);
        private readonly string? _description = GetValidatedDescription(Description);

        public string Code { get => _code; init => _code = GetValidatedCode(value); }
        public string? Description { get => _description; init => _description = GetValidatedDescription(value); }

        private static string GetValidatedCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new InvalidVatCodeException("VAT code cannot be empty.");

            if (code.Length > 4)
                throw new InvalidVatCodeException($"{nameof(code)} is not valid. VAT code are 4 letter codes.");

            return code;
        }

        private static string? GetValidatedDescription(string? description)
        {
            // Todo: Add description validation
            return description;
        }
    }
}