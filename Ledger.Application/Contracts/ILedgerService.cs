using Ledger.Application.ViewModels;

namespace Ledger.Application.Contracts;

public interface ILedgerService
{
    Guid CreateLedger();
    void Deposit(Guid ledgerId, decimal amount);
    void Withdraw(Guid ledgerId, decimal amount);
    void RollBack(Guid ledgerId);
    decimal GetBalance(Guid ledgerId);
    IReadOnlyList<TransactionRecordViewModel> GetTransactionHistory(Guid ledgerId);
    (decimal, List<TransactionRecordViewModel>) GetBalanceForDate(Guid ledgerId, DateTime date);
}