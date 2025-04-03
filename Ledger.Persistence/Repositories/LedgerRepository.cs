using Ledger.Domain;
using Ledger.Domain.Contracts;

namespace Ledger.Persistence.Repositories;

public class LedgerRepository : ILedgerRepository
{
    private readonly Dictionary<Guid, LedgerModel> Ledgers = new();

    public Guid CreateLedger()
    {
        Guid id = Guid.NewGuid();

        if (!Ledgers.TryAdd(id, new LedgerModel()))
        {
            throw new Exception("Ledger not created.");
        }

        return id;
    }

    public LedgerModel GetLedgerById(Guid id)
    {
        if (!Ledgers.TryGetValue(id, out var ledger))
        {
            throw new Exception("Ledger not found.");
        }

        return ledger;
    }

    public void SaveLedger(LedgerModel ledger) => Ledgers[ledger.Id] = ledger;
}
