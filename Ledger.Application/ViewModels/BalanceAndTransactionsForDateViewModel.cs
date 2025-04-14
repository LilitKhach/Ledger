namespace Ledger.Application.ViewModels
{
    public class BalanceAndTransactionsForDateViewModel
    {
        public decimal Balance { get; set; }
        public List<TransactionRecordViewModel> Transactions { get; set; } = new List<TransactionRecordViewModel>();
    }
}