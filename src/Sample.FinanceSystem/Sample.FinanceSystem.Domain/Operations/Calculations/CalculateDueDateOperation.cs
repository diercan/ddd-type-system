﻿using LanguageExt;
using Sample.FinanceSystem.Domain.Operations.Common;
using Sample.FinanceSystem.Domain.Types;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Domain.Operations.Calculations;
internal class CalculateDueDateOperation : InvoiceOperation<UnvalidatedInvoice, UnvalidatedInvoice>
{
    public override EitherAsync<IErrorMessage, UnvalidatedInvoice> Run(UnvalidatedInvoice input, InvoiceContext context)
    {
        throw new NotImplementedException();
    }
}
