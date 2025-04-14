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

    public void RollBack(Guid ledgerId)
    {
        var ledgerTransactions = _ledgerRepository.GetLedgerById(ledgerId);
        var lastOperation = ledgerTransactions.GetTransactionHistory().LastOrDefault()
            ?? throw new NullReferenceException("No transaction is found.");

        var amount = lastOperation.Amount;

        if (amount == 0)
        {
            throw new Exception("Amount can not be 0.");
        }

        if (lastOperation.TransactionType == Domain.TransactionType.Deposit)
        {
            var command = new WithdrawCommand(ledgerTransactions, amount);
            command.Execute();
        }
        else
        {
            var command = new DepositCommand(ledgerTransactions, amount);
            command.Execute();
        }
    }

    public decimal GetBalance(Guid ledgerId)
    {
        var ledger = _ledgerRepository.GetLedgerById(ledgerId);
        return ledger.GetBalance();
    }

    public (decimal, List<TransactionRecordViewModel>) GetBalanceForDate(Guid ledgerId, DateTime date)
    {
        var ledger = _ledgerRepository.GetLedgerById(ledgerId);
        var balance = ledger.GetBalanceForDate(date);
        var transactions = ledger.GetTransactionHistoryForDate(date);
        var historyViewModels = transactions.Select(
            transactionRecord => new TransactionRecordViewModel(
                transactionRecord.TransactionType,
                transactionRecord.Amount,
                transactionRecord.Date
                )).ToList();


        return (balance, historyViewModels);
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
