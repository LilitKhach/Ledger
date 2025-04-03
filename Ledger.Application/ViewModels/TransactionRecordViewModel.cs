using Ledger.Domain;

namespace Ledger.Application.ViewModels;

public record TransactionRecordViewModel(TransactionType TransactionType, decimal Amount, DateTime Date);