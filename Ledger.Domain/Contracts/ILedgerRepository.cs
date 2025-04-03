namespace Ledger.Domain.Contracts;

public interface ILedgerRepository
{
    Guid CreateLedger();
    LedgerModel GetLedgerById(Guid id);
    void SaveLedger(LedgerModel ledger);
}