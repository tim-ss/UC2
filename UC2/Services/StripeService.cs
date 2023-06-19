using Stripe;

namespace UC2.Services
{
    public class StripeService : IStripeService
    {
        private readonly BalanceService balanceService;
        private readonly BalanceTransactionService balanceTransactionService;

        public StripeService()
        {
            var config = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();

            StripeConfiguration.ApiKey = config["StripeApiKey"];
            balanceService = new BalanceService();
            balanceTransactionService = new BalanceTransactionService();
        }

        public Balance GetBalance()
        {
            return balanceService.Get();
        }

        public StripeList<BalanceTransaction> GetBalanceTransactions()
        {
            return balanceTransactionService.List();
        }
    }
}
