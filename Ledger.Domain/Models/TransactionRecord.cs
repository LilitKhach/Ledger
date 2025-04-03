namespace Ledger.Domain;

public record TransactionRecord(Guid Id, TransactionType TransactionType, decimal Amount, DateTime Date);