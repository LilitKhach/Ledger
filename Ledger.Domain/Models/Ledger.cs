using System;

namespace Ledger.Domain;

public class Ledger
{
    public List<TransactionRecord> Transactions { get; set; } = null!;
}
