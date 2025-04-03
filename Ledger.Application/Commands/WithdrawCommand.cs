using Ledger.Application.Contracts;
using Ledger.Domain;

namespace Ledger.Application.Commands;

public class WithdrawCommand : ILedgerCommand
{
    private readonly LedgerModel _ledger;
    private readonly decimal _amount;

    public WithdrawCommand(LedgerModel ledger, decimal amount)
    {
        _ledger = ledger;
        _amount = amount;
    }

    public void Execute()
    {
        _ledger.Withdraw(_amount);
    }
}
