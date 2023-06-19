using Moq;
using Stripe;
using UC2.Controllers;
using UC2.Services;

namespace UC2.Tests
{
    [TestClass]
    public class StripeControllerTest
    {
        private Mock<IStripeService> mockStripeService;

        [TestInitialize]
        public void Setup()
        {
            mockStripeService = new Mock<IStripeService>();

            var balance = new Balance
            {
                Available = new List<BalanceAmount> {
                    new BalanceAmount{Amount = 100, Currency = "tst"},
                    new BalanceAmount{Amount = 200, Currency = "usd"},
                    new BalanceAmount{Amount = 300, Currency = "uah"},
                    new BalanceAmount{Amount = 400, Currency = "aud"},
                    new BalanceAmount{Amount = 500, Currency = "eur"},
                    new BalanceAmount{Amount = 600, Currency = "btc"}
                }
            };
            mockStripeService.Setup(p => p.GetBalance()).Returns(balance);

            StripeList<BalanceTransaction> balanceTransactions = new StripeList<BalanceTransaction>();
            balanceTransactions.Data =new List<BalanceTransaction>
            {
                new BalanceTransaction { Id = "tr1", Amount = 100, Currency = "tst" },
                new BalanceTransaction { Id = "tr2", Amount = 200, Currency = "tst" },
                new BalanceTransaction { Id = "tr3", Amount = 300, Currency = "tst" },
                new BalanceTransaction { Id = "tr4", Amount = 400, Currency = "tst" },
                new BalanceTransaction { Id = "tr5", Amount = 500, Currency = "tst" },
                new BalanceTransaction { Id = "tr6", Amount = 600, Currency = "tst" },
                new BalanceTransaction { Id = "tr7", Amount = 700, Currency = "tst" },
            };
            mockStripeService.Setup(p => p.GetBalanceTransactions()).Returns(balanceTransactions);
        }

        [TestMethod]
        public void TestBalancePagedShouldReturnExactRecordNumber()
        {
            //Arrange
            var controller = new StripeController(mockStripeService.Object);
            var pageNumber = 1;
            var pageSize = 5;

            //Act
            var apiResult = controller.GetBalance(pageNumber, pageSize);

            //Assert
            Assert.IsNotNull(apiResult);
            Assert.IsInstanceOfType(apiResult.Value, typeof(IEnumerable<BalanceAmount>));
            var result = (IEnumerable<BalanceAmount>?)apiResult.Value;

            Assert.AreEqual(5, result?.Count());
        }

        [TestMethod]
        public void TestBalancePagedLargePageSizeShouldReturnAllTheRecords()
        {
            //Arrange
            var controller = new StripeController(mockStripeService.Object);
            var pageNumber = 1;
            var pageSize = 20;

            //Act
            var apiResult = controller.GetBalance(pageNumber, pageSize);

            //Assert
            Assert.IsNotNull(apiResult);
            Assert.IsInstanceOfType(apiResult.Value, typeof(IEnumerable<BalanceAmount>));
            var result = (IEnumerable<BalanceAmount>?)apiResult.Value;

            Assert.AreEqual(6, result?.Count());
        }

        [TestMethod]
        public void TestBalanceTransactionsPagedShouldReturnExactRecordNumber()
        {
            //Arrange
            var controller = new StripeController(mockStripeService.Object);
            var pageNumber = 1;
            var pageSize = 5;

            //Act
            var apiResult = controller.GetBalanceTransactions(pageNumber, pageSize);

            //Assert
            Assert.IsNotNull(apiResult);
            Assert.IsInstanceOfType(apiResult.Value, typeof(IEnumerable<BalanceTransaction>));
            var result = (IEnumerable<BalanceTransaction>?)apiResult.Value;

            Assert.AreEqual(5, result?.Count());
        }

        [TestMethod]
        public void TestBalanceTransactionsPagedLargePageSizeShouldReturnAllTheRecords()
        {
            //Arrange
            var controller = new StripeController(mockStripeService.Object);
            var pageNumber = 1;
            var pageSize = 20;

            //Act
            var apiResult = controller.GetBalanceTransactions(pageNumber, pageSize);

            //Assert
            Assert.IsNotNull(apiResult);
            Assert.IsInstanceOfType(apiResult.Value, typeof(IEnumerable<BalanceTransaction>));
            var result = (IEnumerable<BalanceTransaction>?)apiResult.Value;

            Assert.AreEqual(7, result?.Count());
        }
    }
}