using Sample.FinanceSystem.Domain.Types.CustomerTypes;
using Sample.FinanceSystem.Domain.Types.InvoiceDetailLineTypes;
using Sample.FinanceSystem.Domain.Types.InvoiceTypes;
using Sample.FinanceSystem.Domain.Types.MoneyTypes;
using static Sample.FinanceSystem.Domain.Types.Common.ErrorMessage;
using static Sample.FinanceSystem.Domain.Types.PaymentTypes.Payment;

namespace Sample.FinanceSystem.Domain.Types;
[AsChoice]
public static partial class InvoiceEntity
{
    public interface IInvoice { }

    public record UnvalidatedInvoice : IInvoice
    {
        public DateOnly? CreationDate { get; init; }
        public DateOnly? DueDate { get; init; }
        public Customer? Customer { get; init; }

        public Currency? Currency { get; init; }
        public InvoiceTotal? Total { get; init; }

        public IReadOnlyList<DetailLine>? Lines { get; init; }

        internal UnvalidatedInvoice(
            DateOnly? creationDate,
            Customer? customer,
            DateOnly? dueDate,
            Currency? currency,
            InvoiceTotal? total,
            IEnumerable<DetailLine>? lines)
        {
            CreationDate = creationDate;
            DueDate = dueDate;
            Customer = customer;
            Currency = currency;
            CreationDate = creationDate;
            Total = total;
            Lines = lines?.ToList().AsReadOnly();
        }
    }

    public record CalculatedInvoice : IInvoice
    {
        public DateOnly CreationDate { get; private init; }
        public DateOnly DueDate { get; private init; }
        public Customer Customer { get; private init; }

        public Currency Currency { get; private init; }
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

    public record InvalidInvoice : IInvoice
    {
        public DateOnly? CreationDate { get; private init; }
        public Customer? Customer { get; private init; }

        public IReadOnlyList<DetailLine>? Lines { get; private init; }

        public IReadOnlyCollection<ValidationError> Errors { get; private init; }

        internal InvalidInvoice(
            Customer? customer,
            DateOnly? creationDate,
            IEnumerable<ValidationError> errors,
            IEnumerable<DetailLine>? lines)
        {
            Customer = customer;
            CreationDate = creationDate;
            Lines = lines?.ToList().AsReadOnly();
            Errors = errors.ToList().AsReadOnly();
        }
    }

    public record ApprovedInvoice : IInvoice
    {
        public DateOnly CreationDate { get; private init; }
        public DateOnly DueDate { get; private init; }
        public Customer Customer { get; private init; }
        public InvoiceApproval Approval { get; set; }

        public Currency Currency { get; private init; }
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

    public record PaidInvoice : IInvoice
    {
        public DateOnly CreationDate { get; private init; }
        public DateOnly DueDate { get; private init; }
        public Customer Customer { get; private init; }
        public InvoiceApproval Approval { get; set; }

        public Currency Currency { get; private init; }
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
