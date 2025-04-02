using Ledger.Domain;

namespace Ledger.Application.ViewModels;

public class LedgerViewModel
{
    public List<TransactionRecord> Transactions { get; set; } = null!;
}
