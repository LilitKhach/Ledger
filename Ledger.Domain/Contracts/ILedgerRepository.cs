namespace Ledger.Domain.Contracts
{
    public interface ILedgerRepository
    {
        LedgerModel Deposit(decimal depositAmount);

        LedgerModel Withdraw(decimal withdrawalAmount);
        decimal CurrentBalance();
        void TransactionHistory();
    }
}
