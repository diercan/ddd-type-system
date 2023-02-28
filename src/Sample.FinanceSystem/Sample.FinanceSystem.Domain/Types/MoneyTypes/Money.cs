using System.Globalization;
using LanguageExt;
using Sample.FinanceSystem.Domain.Types.Common;

namespace Sample.FinanceSystem.Domain.Types.MoneyTypes;

public record Money : AbstractDecimalValueType
{
    private static readonly Dictionary<string, CultureInfo> CultureByCurrency = CultureInfo
        .GetCultures(CultureTypes.SpecificCultures)
        .Select(c => new { new RegionInfo(c.Name).ISOCurrencySymbol, Culture = c })
        .GroupBy(a => a.ISOCurrencySymbol, a => a.Culture)
        .ToDictionary(g => g.Key, g => g.First());

    public Currency Currency { get; }
    private CultureInfo Culture { get; }

    protected override string Format => "C";

    public Money(decimal value, Currency currency)
        : base(value)
    {
        Currency = currency;
        Culture = CultureByCurrency.TryGetValue(currency.ToString(), out var culture) ? culture : CultureInfo.CurrentCulture;
    }

    public override string ToString() => Value.ToString(Format, Culture);

    public static bool operator <=(Money left, Money right)
    {
        EnsureSameCurrency(left, right);
        return left.Value <= right.Value;
    }

    public static bool operator >=(Money left, Money right)
    {
        EnsureSameCurrency(left, right);
        return left.Value >= right.Value;
    }

    public static bool operator >(Money left, Money right)
    {
        EnsureSameCurrency(left, right);
        return left.Value > right.Value;
    }

    public static bool operator <(Money left, Money right)
    {
        EnsureSameCurrency(left, right);
        return left.Value < right.Value;
    }

    public static Money operator +(Money left, Money right)
    {
        EnsureSameCurrency(left, right);
        return new Money(left.Value + right.Value, left.Currency);
    }

    public static Money operator -(Money left, Money right)
    {
        EnsureSameCurrency(left, right);
        return new Money(left.Value - right.Value, left.Currency);
    }

    public static Money operator *(Money left, decimal right)
    {
        return new Money(left.Value * right, left.Currency);
    }

    public static Money operator /(Money left, decimal right)
    {
        return new Money(left.Value / right, left.Currency);
    }

    private static void EnsureSameCurrency(Money left, Money right)
    {
        if (left.Currency != right.Currency)
            throw new ArithmeticException("The currency of both arguments must match to perform this operation.");
    }
}
