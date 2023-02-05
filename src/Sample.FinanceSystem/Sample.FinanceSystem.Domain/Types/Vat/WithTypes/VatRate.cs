namespace Sample.FinanceSystem.Domain.Types.Vat.WithTypes
{
    public record VatRate(decimal Value)
    {
        public static readonly VatRate StandardRate_20 = new(20);
        public static readonly VatRate ReducedRate_5 = new(5);
        public static readonly VatRate ZeroRate = new(0);

        private readonly decimal _value = GetValidatedRate(Value);

        public decimal Value { get => _value; init => _value = GetValidatedRate(value); }

        private static decimal GetValidatedRate(decimal value)
        {
            if (value < 0)
            {
                throw new InvalidVatRateException($"{value} is not a valid rate. VAT rate cannot be negative.");
            }

            if (value > 40)
            {
                throw new InvalidVatRateException($"{value} is not a valid rate. VAT rate cannot exceed 40%.");
            }

            return value;
        }
    }
}