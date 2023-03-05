﻿using Sample.FinanceSystem.Domain.Operations.Common;
using Sample.FinanceSystem.Domain.Types;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Domain.Operations.Calculations
{
    internal class CalculateInvoiceTotalsOperation : InvoiceOperation<UnvalidatedInvoice>
    {
        public override IInvoice Run(UnvalidatedInvoice input, InvoiceContext context)
        {
            // Todo: Aggregates all calculate operations.
            throw new NotImplementedException();
        }
    }
}
