using Ledger.Application.Commands;
using Ledger.Application.Contracts;
using Ledger.Application.ViewModels;
using Ledger.Domain.Contracts;

namespace Ledger.Application.Services;

public class LedgerService : ILedgerService
{
    private readonly ILedgerRepository _ledgerRepository;

    public LedgerService(ILedgerRepository ledgerRepository)
    {
        _ledgerRepository = ledgerRepository;
    }

    public Guid CreateLedger()
    {
        return _ledgerRepository.CreateLedger();
    }
    public void Deposit(Guid ledgerId, decimal amount)
    {
        var ledger = _ledgerRepository.GetLedgerById(ledgerId);
        var command = new DepositCommand(ledger, amount);
        command.Execute();
        _ledgerRepository.SaveLedger(ledger);
    }

    public void Withdraw(Guid ledgerId, decimal amount)
    {
        var ledger = _ledgerRepository.GetLedgerById(ledgerId);
        var command = new WithdrawCommand(ledger, amount);
        command.Execute();
        _ledgerRepository.SaveLedger(ledger);
    }

    public decimal GetBalance(Guid ledgerId)
    {
        var ledger = _ledgerRepository.GetLedgerById(ledgerId);
        return ledger.GetBalance();
    }

    public IReadOnlyList<TransactionRecordViewModel> GetTransactionHistory(Guid ledgerId)
    {
        var ledger = _ledgerRepository.GetLedgerById(ledgerId);
        var history = ledger.GetTransactionHistory();
        var historyViewModels = history.Select(
            transactionRecord => new TransactionRecordViewModel(
                transactionRecord.TransactionType,
                transactionRecord.Amount,
                transactionRecord.Date
                )).ToList();

        return historyViewModels.AsReadOnly();
    }
}
