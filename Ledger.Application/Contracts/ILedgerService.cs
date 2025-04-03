using Ledger.Application.ViewModels;

namespace Ledger.Application.Contracts;

public interface ILedgerService
{
    Guid CreateLedger();
    void Deposit(Guid ledgerId, decimal amount);
    void Withdraw(Guid ledgerId, decimal amount);
    decimal GetBalance(Guid ledgerId);
    IReadOnlyList<TransactionRecordViewModel> GetTransactionHistory(Guid ledgerId);
}
