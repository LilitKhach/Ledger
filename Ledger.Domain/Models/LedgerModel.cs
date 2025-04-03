using Ledger.Domain.Models;

namespace Ledger.Domain;

public class LedgerModel : BaseModel
{
    private readonly List<TransactionRecord> TransactionRecords = new();

    public void Deposit(decimal amount)
    {
        TransactionRecords.Add(new TransactionRecord(Guid.NewGuid(), TransactionType.Deposit, amount, DateTime.UtcNow));
    }

    public void Withdraw(decimal amount)
    {
        var currentBalance = GetBalance();

        if(amount > currentBalance)
        {
            throw new InvalidOperationException("Not enough funds.");
        }

        TransactionRecords.Add(new TransactionRecord(Guid.NewGuid(), TransactionType.Withdrawal, amount, DateTime.UtcNow));
    }

    public decimal GetBalance() => TransactionRecords.Sum(tr => tr.TransactionType == TransactionType.Deposit ? tr.Amount : -tr.Amount);
    public IReadOnlyList<TransactionRecord> GetTransactionHistory() => TransactionRecords.AsReadOnly();
}
