namespace Sample.FinanceSystem.Domain.Types.MoneyTypes;

/// <summary>
/// Represents world currency by numeric and alphabetic values, as per ISO 4217:
/// https://en.wikipedia.org/wiki/ISO_4217.
/// </summary>
public enum Currency : ushort
{
    USD = 840,
    EUR = 978,
    GBP = 826,
    RON = 946
}
