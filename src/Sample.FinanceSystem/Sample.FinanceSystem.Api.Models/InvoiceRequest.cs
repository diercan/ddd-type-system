using System.ComponentModel.DataAnnotations;

namespace Sample.FinanceSystem.Api.Models;

public record InvoiceRequest(
    DateOnly? CreationDate,
    DateOnly? DueDate,
    string? CustomerName,
    [Required]
    [StringLength(maximumLength:5)]
    string? CustomerCode,
    string? VatRegistrationNumber,
    string? CurrencyCode,
    IReadOnlyList<InvoiceLineDto> Lines);
