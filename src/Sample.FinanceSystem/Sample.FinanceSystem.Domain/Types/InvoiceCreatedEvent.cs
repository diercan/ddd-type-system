namespace Sample.FinanceSystem.Domain.Types;
[AsChoice]
public static partial class InvoiceCreatedEvent
{
    public interface IInvoiceCreatedEvent { }
    public record InvoiceCreatedSuccessfulyEvent : IInvoiceCreatedEvent { }
    public record InvoiceCreateFailValidationEvent : IInvoiceCreatedEvent { }
    public record InvoiceCreateUnexpectedErrorEvent : IInvoiceCreatedEvent { }
}
