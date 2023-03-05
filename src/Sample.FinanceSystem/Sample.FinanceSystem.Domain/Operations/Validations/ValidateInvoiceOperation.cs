using System.Collections.Immutable;
using LanguageExt;
using Sample.FinanceSystem.Domain.Operations.Common;
using Sample.FinanceSystem.Domain.Types;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;
using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Domain.Operations.Validations
{
    internal class ValidateInvoiceOperation : InvoiceOperation<UnvalidatedInvoice>
    {
        public override IInvoice Run(UnvalidatedInvoice input, InvoiceContext context)
        {
            IEnumerable<ValidationError> errors = ValidateDefaults(input).ToImmutableList();

            // Todo: We need to support multiple validation errors

            return errors.Any()
                ? new InvalidInvoice(input, errors.First())
                : input;
        }

        public static IEnumerable<ValidationError> ValidateDefaults(UnvalidatedInvoice invoice)
        {
            if (!ValidateCurrency(invoice, out ValidationError? invalidCurrency) && invalidCurrency != null)
                yield return invalidCurrency;

            if (!ValidateVat(invoice, out ValidationError? invalidVat) && invalidVat != null)
                yield return invalidVat;
        }

        public static bool ValidateVat(UnvalidatedInvoice invoice, out ValidationError? error)
        {
            throw new NotImplementedException();
        }

        public static bool ValidateCurrency(UnvalidatedInvoice invoice, out ValidationError? error)
        {
            throw new NotImplementedException();
        }
    }
}
