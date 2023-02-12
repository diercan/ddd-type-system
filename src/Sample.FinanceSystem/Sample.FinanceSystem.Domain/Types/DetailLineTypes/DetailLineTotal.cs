using Sample.FinanceSystem.Domain.Types.MoneyTypes;

namespace Sample.FinanceSystem.Domain.Types.DetailLineTypes;

public record DetailLineTotal(Money Gross, Money Net, Money Tax);
