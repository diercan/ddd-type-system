namespace Sample.FinanceSystem.Api.Models;

public record InvoiceLineDto(
    string Description,
    string LedgerCore,
    string VatCode,
    decimal Gross,
    decimal Net,
    decimal Tax);
