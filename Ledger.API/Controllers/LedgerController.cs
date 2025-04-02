using Ledger.Application.ViewModels;
using Ledger.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Ledger.API;

[ApiController]
[Route("api/[controller]")]
public class LedgerController : ControllerBase
{
    [HttpPost("/deposit")]
    public LedgerViewModel Deposit(decimal transactionAmount)
    {
        var transactionRecord = new TransactionRecord ( 1, TransactionType.Deposit, 100, DateTime.Now );

        return new LedgerViewModel { };
    }
}
