using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace UC2.Controllers;

[ApiController]
[Route("[controller]")]
public class StripeController : ControllerBase
{
    private IConfiguration _configuration { get; }

    public StripeController(IConfiguration configuration)
    {
        _configuration = configuration;
        StripeConfiguration.ApiKey = _configuration.GetValue<string>("ApiKeys:Stripe");
    }

    [Route("balance")]
    [HttpGet(Name = "balance")]
    public ObjectResult GetBalance()
    {
        var service = new BalanceService();
        
        try
        {
            var balance = service.Get();
            return Ok(balance);

        }
        catch (Exception ex)
        {
            var error = new { ErrorMessage = ex.Message };
            return Ok(error);
        }
    }
}
