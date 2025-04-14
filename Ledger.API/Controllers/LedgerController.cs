using Ledger.Application.Contracts;
using Ledger.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ledger.API;

[ApiController]
[Route("api/[controller]")]
public class LedgerController : ControllerBase
{
    private readonly ILedgerService _ledgerService;
    public LedgerController(ILedgerService ledgerService)
    {
        _ledgerService = ledgerService;
    }

    [HttpPost]
    public IActionResult CreateLedger()
    {
        var id = _ledgerService.CreateLedger();
        return Ok(id);
    }

    [HttpPost("{ledgerId}/deposit")]
    public IActionResult Deposit([FromRoute] Guid ledgerId, decimal transactionAmount)
    {
        if (transactionAmount == 0)
        {
            return BadRequest("Deposit amount cannot be 0.");
        }
        try
        {
            _ledgerService.Deposit(ledgerId, transactionAmount);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to deposit: {ex.Message}");
        }

        return Ok();
    }

    [HttpPost("{ledgerId}/withdraw")]
    public IActionResult Withdraw([FromRoute] Guid ledgerId, decimal transactionAmount)
    {
        if (transactionAmount == 0)
        {
            return BadRequest("Deposit amount cannot be 0.");
        }

        try
        {
            _ledgerService.Withdraw(ledgerId, transactionAmount);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to withdraw: {ex.Message}");
        }

        return Ok();
    }

    [HttpPost]
    [Route("{ledgerId}/revert")]
    public IActionResult RollBack([FromRoute] Guid ledgerId)
    {
        try
        {
            _ledgerService.RollBack(ledgerId);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to revert: {ex.Message}");
        }
        return Ok();
    }

    [HttpGet("{ledgerId}/balance")]
    public decimal GetBalance([FromRoute] Guid ledgerId)
    {
        return _ledgerService.GetBalance(ledgerId);
    }

    [HttpGet("{ledgerId}/history")]
    public IReadOnlyList<TransactionRecordViewModel> GetTransactionHistory([FromRoute] Guid ledgerId)
    {
        return _ledgerService.GetTransactionHistory(ledgerId);
    }

    [HttpGet("{ledgerId}/balance/{date}")]
    public BalanceAndTransactionsForDateViewModel GetBalanceForDate([FromRoute] Guid ledgerId, DateTime date)
    {
        var (balance, transactions) = _ledgerService.GetBalanceForDate(ledgerId, date);
        return new BalanceAndTransactionsForDateViewModel { Balance = balance, Transactions = transactions };
    }
}