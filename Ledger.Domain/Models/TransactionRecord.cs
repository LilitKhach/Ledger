namespace Ledger.Domain;

public record TransactionRecord(int Id, TransactionType TransactionType, decimal Amount, DateTime Date);