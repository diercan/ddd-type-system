namespace Sample.FinanceSystem.Domain.WhatsWrong;
public class DetailLine
{
    public string Descriptipon { get; set; } //max string size 200
    public string VatCode { get; set; } //Vat Code and Percentage are linked
    public decimal VatPercentage { get; set; }

    //Gross, Net, Tax are linked, decimal is to wide as a range
    public decimal Gross { get; set; }
    public decimal Net { get; set; }
    public decimal Tax { get; set; }

    //there is a pattern
    public string LedgerCode { get; set; }
}
