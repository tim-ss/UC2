using Microsoft.AspNetCore.Mvc;
using UC2.Services;

namespace UC2.Controllers;

[ApiController]
[Route("[controller]")]
public class StripeController : ControllerBase
{
    private readonly IStripeService stripeService;

    public StripeController(IStripeService stripeService)
    {
        this.stripeService = stripeService;
    }

    [Route("balance")]
    [HttpGet(Name = "balance")]
    public ObjectResult GetBalance(int pageNumber, int pageSize)
    {
        try
        {
            var balanceList = stripeService.GetBalance();
            var skip = pageSize * (pageNumber - 1);
            var pageData = balanceList.Available.Skip(skip).Take(pageSize);
            return Ok(pageData);
        }
        catch
        {
            throw;
        }
    }

    [Route("balancetransactions")]
    [HttpGet(Name = "balance_page")]
    public ObjectResult GetBalancePaged(int pageNumber, int pageSize)
    {
        try
        {
            var transactionsList = stripeService.GetBalanceTransactions();
            var skip = pageSize * (pageNumber - 1);
            var pageData = transactionsList.Skip(skip).Take(pageSize);
            return Ok(pageData);
        }
        catch
        {
            throw;
        }
    }

}
