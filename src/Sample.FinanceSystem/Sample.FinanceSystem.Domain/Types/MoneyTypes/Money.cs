using LanguageExt;
using Sample.FinanceSystem.Domain.Types.Common;
using System.Globalization;

namespace Sample.FinanceSystem.Domain.Types.MoneyTypes;

public record Money : AbstractDecimalValueType, IDecimalValueType<Money>
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

    // Todo: For Money, do we need Parse methods? Not sure what constraints to apply to the amounts
    //       Might just want to focus here on the fact that should not be able to do operations on amounts with different currencies
    //       and also that any operation results in another Money object with same currency.
    public static Either<ErrorMessage, Money> Parse(decimal value)
        => Parse(value, Currency.RON);

    public static Either<ErrorMessage, Money> Parse(decimal value, Currency currency)
    {
        return new Money(value, currency);
    }

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
