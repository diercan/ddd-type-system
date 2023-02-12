using Sample.FinanceSystem.Domain.Types.Common;
using Sample.FinanceSystem.Domain.Types.CustomerTypes;
using Sample.FinanceSystem.Domain.Types.InvoiceDetailLineTypes;
using Sample.FinanceSystem.Domain.Types.InvoiceTypes;
using static Sample.FinanceSystem.Domain.Types.PaymentTypes.Payment;

namespace Sample.FinanceSystem.Domain.Types;
[AsChoice]
public static partial class Invoice
{
    public interface IInvoices { }

    public record UnvalidatedInvoice : IInvoices
    {
        public DateOnly CreationDate { get; private init; }
        public Customer Customer { get; private init; }

        public IReadOnlyList<DetailLine> Lines { get; private init; }

        internal UnvalidatedInvoice(
            Customer customer,
            DateOnly creationDate,
            IEnumerable<DetailLine> lines)
        {
            Customer = customer;
            CreationDate = creationDate;
            Lines = lines.ToList().AsReadOnly();
        }
    }

    public record CalculatedInvoice : IInvoices
    {
        public DateOnly CreationDate { get; private init; }
        public DateOnly DueDate { get; private init; }
        public Customer Customer { get; private init; }

        public InvoiceTotal Total { get; private init; }

        public IReadOnlyList<DetailLine> Lines { get; private init; }

        internal CalculatedInvoice(
            Customer customer,
            DateOnly creationDate,
            DateOnly dueDate,
            InvoiceTotal total,
            IEnumerable<DetailLine> lines)
        {
            Customer = customer;
            CreationDate = creationDate;
            DueDate = dueDate;
            Total = total;
            Lines = lines.ToList().AsReadOnly();
        }
    }

    public record InvalidInvoice : IInvoices
    {
        public DateOnly CreationDate { get; private init; }
        public Customer Customer { get; private init; }

        public IReadOnlyList<DetailLine> Lines { get; private init; }

        public IReadOnlyCollection<ErrorMessage> Errors { get; private init; }

        internal InvalidInvoice(
            Customer customer,
            DateOnly creationDate,
            IEnumerable<ErrorMessage> errors,
            IEnumerable<DetailLine> lines)
        {
            Customer = customer;
            CreationDate = creationDate;
            Lines = lines.ToList().AsReadOnly();
            Errors = errors.ToList().AsReadOnly();
        }
    }

    public record ApprovedInvoice : IInvoices
    {
        public DateOnly CreationDate { get; private init; }
        public DateOnly DueDate { get; private init; }
        public Customer Customer { get; private init; }
        public InvoiceApproval Approval { get; set; }

        public InvoiceTotal Total { get; private init; }

        public IReadOnlyList<DetailLine> Lines { get; private init; }

        internal ApprovedInvoice(
            Customer customer,
            DateOnly creationDate,
            DateOnly dueDate,
            InvoiceApproval approval,
            InvoiceTotal total,
            IEnumerable<DetailLine> lines)
        {
            Customer = customer;
            CreationDate = creationDate;
            DueDate = dueDate;
            Total = total;
            Approval = approval;
            Lines = lines.ToList().AsReadOnly();
        }
    }

    public record PaidInvoice : IInvoices
    {
        public DateOnly CreationDate { get; private init; }
        public DateOnly DueDate { get; private init; }
        public Customer Customer { get; private init; }
        public InvoiceApproval Approval { get; set; }
        
        public InvoiceTotal Total { get; private init; }

        public IReadOnlyList<DetailLine> Lines { get; private init; }

        public IPayment Payment { get; private init; }

        internal PaidInvoice(
            Customer customer,
            DateOnly creationDate,
            DateOnly dueDate,
            InvoiceApproval approval,
            InvoiceTotal total,
            IPayment payment,
            IEnumerable<DetailLine> lines)
        {
            Customer = customer;
            CreationDate = creationDate;
            DueDate = dueDate;
            Approval = approval;
            Total = total;
            Lines = lines.ToList().AsReadOnly();
            Payment = payment;
        }
    }
}
