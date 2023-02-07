namespace Sample.FinanceSystem.Domain.Types
{
    [AsChoice]
    public static partial class Invoice
    {
        public interface IInvoices { }

        public record UnvalidatedInvoice : IInvoices { }

        public record CalculatedInvoice : IInvoices { }

        public record InvalidatedInvoice : IInvoices { }

        public record WaitingInvoice : IInvoices { }

        public record ApprovedInvoice : IInvoices { }
    }
}
