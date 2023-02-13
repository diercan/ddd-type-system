﻿using Sample.FinanceSystem.Domain.Types.MoneyTypes;

namespace Sample.FinanceSystem.Domain.Types.InvoiceTypes;
public record InvoiceTotal(Money Gross, Money Net, Money Tax);
