using Sample.FinanceSystem.Domain.Types.Common;
using Sample.FinanceSystem.Domain.Types.CustomerTypes;
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

        //TODO add details

        internal UnvalidatedInvoice(Customer customer, DateOnly creationDate)
        {
            Customer = customer;
            CreationDate = creationDate;
        }
    }

    public record CalculatedInvoice : IInvoices
    {
        public DateOnly CreationDate { get; private init; }
        public DateOnly DueDate { get; private init; }
        public Customer Customer { get; private init; }
        //TODO use money type
        public InvoiceTotal Total { get; private init; }

        //TODO add details

        internal CalculatedInvoice(
            Customer customer,
            DateOnly creationDate,
            DateOnly dueDate,
            InvoiceTotal total)
        {
            Customer = customer;
            CreationDate = creationDate;
            DueDate = dueDate;
            Total = total;
        }
    }

    public record InvalidInvoice : IInvoices
    {
        public DateOnly CreationDate { get; private init; }
        public Customer Customer { get; private init; }

        //TODO add details

        public IReadOnlyCollection<ErrorMessage> Errors { get; private init; }

        internal InvalidInvoice(
            Customer customer,
            DateOnly creationDate,
            IEnumerable<ErrorMessage> errors)
        {
            Customer = customer;
            CreationDate = creationDate;
            Errors = errors.ToList().AsReadOnly();
        }
    }

    public record ApprovedInvoice : IInvoices
    {
        public DateOnly CreationDate { get; private init; }
        public DateOnly DueDate { get; private init; }
        public Customer Customer { get; private init; }
        public InvoiceApproval Approval { get; set; }
        //TODO use money type
        public InvoiceTotal Total { get; private init; }

        //TODO add details

        internal ApprovedInvoice(
            Customer customer,
            DateOnly creationDate,
            DateOnly dueDate,
            InvoiceApproval approval,
            InvoiceTotal total)
        {
            Customer = customer;
            CreationDate = creationDate;
            DueDate = dueDate;
            Total = total;
            Approval = approval;
        }
    }

    public record PaidInvoice : IInvoices
    {
        public DateOnly CreationDate { get; private init; }
        public DateOnly DueDate { get; private init; }
        public Customer Customer { get; private init; }
        public InvoiceApproval Approval { get; set; }
        //TODO use money type
        public InvoiceTotal Total { get; private init; }
        public IPayment Payment { get; private init; }

        //TODO add details

        internal PaidInvoice(
            Customer customer,
            DateOnly creationDate,
            DateOnly dueDate,
            InvoiceApproval approval,
            InvoiceTotal total,
            IPayment payment)
        {
            Customer = customer;
            CreationDate = creationDate;
            DueDate = dueDate;
            Approval = approval;
            Total = total;
            Payment = payment;
        }
    }
}
