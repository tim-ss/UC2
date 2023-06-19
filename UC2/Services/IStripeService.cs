using Stripe;

namespace UC2.Services
{
    public interface IStripeService
    {
        Balance GetBalance();
        StripeList<BalanceTransaction> GetBalanceTransactions();
    }
}
