namespace Sample.FinanceSystem.Domain.Types.Vat.WithPrimitives
{
    public record Vat(string Code, string Description, decimal Rate)
    {
        private readonly string _code = GetValidatedCode(Code);
        private readonly string _description = GetValidatedDescription(Description);
        private readonly decimal _rate = GetValidatedRate(Rate);

        public string Code { get => _code; init => _code = GetValidatedCode(value); }
        public string Description { get => _description; init => _description = GetValidatedDescription(value); }
        public decimal Rate { get => _rate; init => _rate = GetValidatedRate(value); }

        private static string GetValidatedCode(string code)
        {
            // Todo: Add code validation
            return code;
        }

        private static string GetValidatedDescription(string description)
        {
            // Todo: Add description validation
            return description;
        }

        private static decimal GetValidatedRate(decimal value)
        {
            if (value < 0)
            {
                throw new InvalidVatRateException($"{value} is not a valid rate. VAT rate cannot be negative.");
            }

            if (value > 100)
            {
                throw new InvalidVatRateException($"{value} is not a valid rate. VAT rate cannot exceed 100%.");
            }

            return value;
        }
    }
}
