using System.ComponentModel.DataAnnotations;

namespace Sample.FinanceSystem.Api.Models;

public record InvoiceDto(
    DateOnly CreationDate,
    DateOnly DueDate,
    string CustomerName,
    string CustomerCode,
    string VatRegistrationNumber,
    string CurrencyCode,
    IReadOnlyList<InvoiceLineDto> Lines);