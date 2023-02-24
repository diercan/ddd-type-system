using static Sample.FinanceSystem.Domain.Types.InvoiceEntity;

namespace Sample.FinanceSystem.Domain.Types;

[AsChoice]
public static partial class InvoiceCreatedEvent
{
    public interface IInvoiceCreatedEvent { }
    public record InvoiceCreatedSuccessfulyEvent(int InvoiceId, ValidatedInvoice Invoice) : IInvoiceCreatedEvent { }
    public record InvoiceCreateFailValidationEvent(IReadOnlyDictionary<string, string> Errors) : IInvoiceCreatedEvent { }
    public record InvoiceCreateUnexpectedErrorEvent(string ErrorMessage, Exception? Exception) : IInvoiceCreatedEvent { }
}
