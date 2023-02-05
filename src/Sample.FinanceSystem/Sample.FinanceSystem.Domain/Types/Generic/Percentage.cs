namespace Sample.FinanceSystem.Domain.Types.Generic
{
    public record Percentage(decimal Value)
    {

        private readonly decimal _value = GetValidatedRate(Value);

        public decimal Value { get => _value; init => _value = GetValidatedRate(value); }

        private static decimal GetValidatedRate(decimal value)
        {
            return value;
        }
    }
}
