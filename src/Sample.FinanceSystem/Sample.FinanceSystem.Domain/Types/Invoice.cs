using Sample.FinanceSystem.Domain.Types.CustomerTypes;
using Sample.FinanceSystem.Domain.Types.InvoiceDetailLineTypes;
using Sample.FinanceSystem.Domain.Types.InvoiceTypes;
using Sample.FinanceSystem.Domain.Types.MoneyTypes;
using static Sample.FinanceSystem.Domain.Types.PaymentTypes.Payment;

namespace Sample.FinanceSystem.Domain.Types;

[AsChoice]
public static partial class InvoiceEntity
{
    public interface IInvoice { }

    public record UnvalidatedInvoice : IInvoice
    {
        public DateOnly? CreationDate { get; internal init; }
        public DateOnly? DueDate { get; internal init; }
        public UnvalidatedCustomer Customer { get; internal init; }

        public Currency? Currency { get; internal init; }
        public UnvalidatedInvoiceTotal Total { get; internal init; }

        public IReadOnlyList<UnvalidatedDetailLine> Lines { get; internal init; }

        internal UnvalidatedInvoice(
           DateOnly? creationDate,
           DateOnly? dueDate,
           UnvalidatedCustomer customer,
           Currency? currency,
           UnvalidatedInvoiceTotal total,
           IEnumerable<UnvalidatedDetailLine> lines)
        {
            CreationDate = creationDate;
            DueDate = dueDate;
            Customer = customer;
            Currency = currency;
            Total = total;
            Lines = lines.ToList().AsReadOnly();
        }
    }

    public record ValidatedInvoice : IInvoice
    {
        public DateOnly CreationDate { get; internal init; }
        public DateOnly DueDate { get; internal init; }
        public Customer Customer { get; internal init; }

        public Currency Currency { get; internal init; }
        public InvoiceTotal Total { get; internal init; }

        public IReadOnlyList<DetailLine> Lines { get; internal init; }

        internal ValidatedInvoice(
            DateOnly creationDate,
            DateOnly dueDate,
            Customer customer,
            Currency currency,
            InvoiceTotal total,
            IEnumerable<DetailLine> lines)
        {
            CreationDate = creationDate;
            DueDate = dueDate;
            Customer = customer;
            Currency = currency;
            Total = total;
            Lines = lines.ToList().AsReadOnly();
        }
    }

    public record ApprovedInvoice : IInvoice
    {
        public DateOnly CreationDate { get; internal init; }
        public DateOnly DueDate { get; internal init; }
        public Customer Customer { get; internal init; }
        public InvoiceApproval Approval { get; internal set; }

        public Currency Currency { get; internal init; }
        public InvoiceTotal Total { get; internal init; }

        public IReadOnlyList<DetailLine> Lines { get; internal init; }

        internal ApprovedInvoice(
            DateOnly creationDate,
            DateOnly dueDate,
            Customer customer,
            Currency currency,
            InvoiceApproval approval,
            InvoiceTotal total,
            IEnumerable<DetailLine> lines)
        {
            CreationDate = creationDate;
            DueDate = dueDate;
            Customer = customer;
            Approval = approval;
            Currency = currency;
            Total = total;
            Lines = lines.ToList().AsReadOnly();
        }
    }

    public record PaidInvoice : IInvoice
    {
        public DateOnly CreationDate { get; internal init; }
        public DateOnly DueDate { get; internal init; }
        public Customer Customer { get; internal init; }
        public InvoiceApproval Approval { get; internal set; }

        public Currency Currency { get; internal init; }
        public InvoiceTotal Total { get; internal init; }

        public IReadOnlyList<DetailLine> Lines { get; internal init; }

        public IPayment Payment { get; internal init; }

        internal PaidInvoice(
            DateOnly creationDate,
            DateOnly dueDate,
            Customer customer,
            Currency currency,
            InvoiceApproval approval,
            InvoiceTotal total,
            IPayment payment,
            IEnumerable<DetailLine> lines)
        {
            CreationDate = creationDate;
            DueDate = dueDate;
            Customer = customer;
            Approval = approval;
            Currency = currency;
            Total = total;
            Lines = lines.ToList().AsReadOnly();
            Payment = payment;
        }
    }
}
