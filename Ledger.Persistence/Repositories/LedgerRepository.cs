using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Ledger.Domain;

namespace Ledger.Persistence.Repositories
{
    public class LedgerRepository
    {
        private List<TransactionRecord> TransactionRecords = new List<TransactionRecord>();

        public LedgerModel Deposit(decimal depositAmount)
        {
            var transactionRecord = new TransactionRecord(Guid.NewGuid(), TransactionType.Deposit, 100, DateTime.Now);
            TransactionRecords.Add(transactionRecord);

            return new LedgerModel { TransactionRecords = this.TransactionRecords };
        }

        public LedgerModel Withdraw(decimal withdrawalAmount)
        {
            decimal currentBalance = CurrentBalance();
            if (currentBalance < withdrawalAmount)
            {
                throw new InvalidOperationException("Insufficient funds");
            }

            var transactionRecord = new TransactionRecord(Guid.NewGuid(), TransactionType.Withdrawal, withdrawalAmount, DateTime.Now);
            TransactionRecords.Add(transactionRecord);

            return new LedgerModel { TransactionRecords = this.TransactionRecords};
        }

        public decimal CurrentBalance()
        {
            return TransactionRecords.Sum(tr => tr.TransactionType == TransactionType.Deposit ? tr.Amount : -tr.Amount);
        }

        public void TransactionHistory()
        {
            foreach (var transactionRecord in TransactionRecords)
            {
                Console.WriteLine($"{transactionRecord.Date} - {transactionRecord.TransactionType}: ${transactionRecord.Amount}");
            }
        }
    }
}
