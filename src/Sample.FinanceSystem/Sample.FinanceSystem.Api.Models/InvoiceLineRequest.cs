using System.ComponentModel.DataAnnotations;

namespace Sample.FinanceSystem.Api.Models;

internal record InvoiceLineRequest(
    [Required]
    [StringLength(maximumLength: 200)]
    string? Description,
    [Required]
    [StringLength(maximumLength: 6, MinimumLength = 6)]
    string? LedgerCore,
    [Required]
    [StringLength(maximumLength: 10)]
    string? VatCode,
    decimal? Gross,
    decimal? Net,
    decimal? Tax);
