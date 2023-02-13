namespace Sample.FinanceSystem.Domain.Types;
[AsChoice]
public static class InvoiceCreatedEvent
{
    public interface IInvoiceCreatedEvent { }
    public record InvoiceCreatedSuccessfulyEvent : IInvoiceCreatedEvent { }
    public record InvoiceCreateFailValidationEvent : IInvoiceCreatedEvent { }
    public record InvoiceCreateUnexpectedErrorEvent : IInvoiceCreatedEvent { }
}
