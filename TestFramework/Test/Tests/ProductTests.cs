using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Linq;
using TestFramework.Driver;
using TestFramework.Environment;
using TestFramework.Page;
using TestFramework.Test.Base;
using TestFramework.Test.Services;
using System.Text.RegularExpressions;
using System.Threading;

namespace TestFramework.Test.Tests
{
    [TestFixture]
    public class ProductTests : TestBase
    {
        private IWebDriver _driver;
        private IConfiguration _configuration;

        [SetUp]
        public void Setup()
        {
            _configuration = Settings.GetConfiguration();
            _driver = DriverController.Driver(Settings.Browser);
            _driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void CloseBrowser()
        {
            DriverController.CloseBrowser();
        }

        [Test]
        public void SearchTest()
        {
            Test(() =>
            {
                var product = ProductCreator.CreateProduct(_configuration);
                var homePage = new HomePage(_driver);
                var products = homePage
                    .GoTo()
                    .GoToItemsPage()
                    .SearchProduct(product.Type + " " + product.Brand);

                bool brandTrue = true;
                bool typeTrue = true;
                Regex regexBrands = new Regex(".*mango.*");
                Regex regexTypes = new Regex(".*дж(егг)*инс.*");
                var brands = products
                    .GetBrandNames();
                var types = products
                    .GetItemsTypes();
                foreach (var brand in brands)
                {
                    if (!regexBrands.IsMatch(brand))
                        brandTrue = false;
                }
                foreach (var type in types)
                {
                    if (!regexTypes.IsMatch(type))
                        typeTrue = false;
                }

                Assert.IsTrue(brandTrue && typeTrue);
            });
        }

        [Test]
        public void PricesFilterTest()
        {
            Test(() =>
            {
                var product = ProductCreator.CreateProduct(_configuration);
                var homePage = new HomePage(_driver);
                bool pricesFilterIsWorking = homePage
                    .GoTo()
                    .GoToItemsPage()
                    .PricesFilterCheck(product.MinPrice, product.MaxPrice);

                Assert.IsTrue(pricesFilterIsWorking);
            });
        }

        [Test]
        public void AddToCartTest()
        {
            Test(() =>
            {
                var homePage = new HomePage(_driver);
                string productTitleOne = homePage
                    .GoTo()
                    .GoToItemsPage()
                    .OpenItem()
                    .GetItemName();
                string productTitleTwo = homePage
                    .GoTo()
                    .GoToItemsPage()
                    .OpenItem()
                    .AddToCart()
                    .GoToCartPage()
                    .GetTitle();

                homePage
                    .GoToCartPage()
                    .EmptyCart();

                Assert.IsTrue(productTitleOne == productTitleTwo);
            });
        }

        [Test]
        public void AddToWishlistTest()
        {
            Test(() =>
            {
                var user = UserCreator.CreateUser(_configuration);
                var homePage = new HomePage(_driver);
                string productTitleOne = homePage
                    .GoTo()
                    .GoToItemsPage()
                    .OpenItem()
                    .GetItemName();
                string productTitleTwo = homePage
                    .GoTo()
                    .GoToLoginPage(user)
                    .GoToItemsPage()
                    .OpenItem()
                    .AddToWishlist()
                    .GoToHomePage()
                    .GoToWishlistPage()
                    .GetItemName();
                homePage
                    .GoToWishlistPage()
                    .RemoveFromWishlist();

                Assert.IsTrue(productTitleOne == productTitleTwo);
            });
        }

        [Test]
        public void SalesTest()
        {
            Test(() =>
            {
                var homePage = new HomePage(_driver);
                var isOnSales = homePage
                    .GoTo()
                    .GoToSalesPage()
                    .AreAllItemsOnSale();

                Assert.IsTrue(isOnSales);
            });
        }

        [Test]
        public void ClearCartTest()
        {
            Test(() =>
            {
                var product = ProductCreator.CreateProduct(_configuration);
                var homePage = new HomePage(_driver);
                string cartText = homePage
                    .GoTo()
                    .GoToItemsPage()
                    .OpenItem()
                    .AddToCart()
                    .GoToCartPage()
                    .EmptyCart()
                    .EmptyCartText();

                Assert.AreEqual(cartText, product.ClearCartText);
            });
        }
    }
}
