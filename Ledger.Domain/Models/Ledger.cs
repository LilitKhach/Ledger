using System;

namespace Ledger.Domain;

public class LedgerModel
{
    public List<TransactionRecord> TransactionRecords { get; set; } = null!;
}
