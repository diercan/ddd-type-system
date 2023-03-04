using Sample.FinanceSystem.Domain.Types.Common;
using Sample.FinanceSystem.Domain.Types.InvoiceDetailLineTypes;

namespace Sample.FinanceSystem.Domain.Types.DetailLineTypes;

public record DetailLines : ReadOnlyListRecord<DetailLine>
{
    public DetailLines(IEnumerable<DetailLine> lines) : base(lines) { }
}

public record UnvalidatedDetailLines : ReadOnlyListRecord<UnvalidatedDetailLine>
{
    public UnvalidatedDetailLines(IEnumerable<UnvalidatedDetailLine> lines) : base(lines) { }
}

public static class Ex
{
    public static UnvalidatedDetailLines AsDetailLines(this IEnumerable<UnvalidatedDetailLine> Lines) => new(Lines);
}
