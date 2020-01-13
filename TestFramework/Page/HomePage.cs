using OpenQA.Selenium;
using System.Threading;
using TestFramework.Constants;
using TestFramework.Page.Base;
using TestFramework.Test.Models;

namespace TestFramework.Page
{
    public class HomePage : SearchBarPageBase<HomePage>
    {
        private const string _loginButton = "/html/body/div[1]/div[2]/div[1]/span[1]";
        private const string _loginConfirmButton = "/html/body/div[2]/div/div/div[2]/form[1]/div[6]/button";
        private const string _profileButton = "/html/body/div[1]/div[2]/div[1]/span[1]/a";
        private const string _profileInfoButton = "//*[@id='vue-root']/div[3]/div/div[1]/div/div/div/ul/li[3]/a";
        private const string _emailField = "/html/body/div[2]/div/div/div[2]/form[1]/div[2]/div/input";
        private const string _passwordField = "/html/body/div[2]/div/div/div[2]/form[1]/div[3]/div/input";

        private const string _regionBtn = "/html/body/div[1]/div[1]/div/div/div/div/span/span[2]";
        private const string _regionNew = "/html/body/div[10]/div/div/div/div[2]/form/ul[2]/li[2]/span";
        private const string _regionConfirm = "/html/body/div[10]/div/div/div/div[2]/form/div[4]/button/span";

        public HomePage(IWebDriver driver)
            : base(driver)
        {
            Url = PageUrl.Home;
        }

        public HomePage GoTo()
        {
            _driver.Navigate().GoToUrl(Url);
            return this;
        }

        public HomePage GoToLoginPage(User user)
        {
            var loginButton = _driver.FindElement(By.XPath(_loginButton));
            loginButton.Click();
            Thread.Sleep(500);

            var emailField = _driver.FindElement(By.XPath(_emailField));
            ScrollToElement(emailField);
            emailField.SendKeys(user.Email);
            var passwordField = _driver.FindElement(By.XPath(_passwordField));
            ScrollToElement(passwordField);
            passwordField.SendKeys(user.Password);
            var loginConfirmButton = _driver.FindElement(By.XPath(_loginConfirmButton));
            ScrollToElement(loginConfirmButton);
            loginConfirmButton.Click();
            Thread.Sleep(30000);

            return this;
        }

        public ProfilePage GoToProfilePage()
        {
            Thread.Sleep(1000);
            var profileButton = _driver.FindElement(By.XPath(_profileButton));
            Thread.Sleep(500);
            profileButton.Click();
            Thread.Sleep(1000);
            var profileInfoButton = _driver.FindElement(By.XPath(_profileInfoButton));
            Thread.Sleep(500);
            profileInfoButton.Click();
            Thread.Sleep(1000);
            /*_driver.Navigate().GoToUrl(PageUrl.Profile);
            Thread.Sleep(1000);*/
            return new ProfilePage(_driver);
        }

        public ItemsPage GoToItemsPage()
        {
            _driver.Navigate().GoToUrl(PageUrl.Items);
            Thread.Sleep(1000);
            return new ItemsPage(_driver);
        }

        public WishlistPage GoToWishlistPage()
        {
            _driver.Navigate().GoToUrl(PageUrl.Wishlist);
            Thread.Sleep(1000);
            return new WishlistPage(_driver);
        }

        public SalesPage GoToSalesPage()
        {
            _driver.Navigate().GoToUrl(PageUrl.Sales);
            Thread.Sleep(1000);
            return new SalesPage(_driver);
        }

        public CartPage GoToCartPage()
        {
            _driver.Navigate().GoToUrl(PageUrl.Cart);
            Thread.Sleep(1000);
            return new CartPage(_driver);
        }

        public string GetProfileText()
        {
            var profileBtn = _driver.FindElement(By.XPath(_profileButton));
            return profileBtn.Text;
        }

        public HomePage ChangeRegion()
        {
            Thread.Sleep(1000);
            var regionSelection = _driver.FindElement(By.XPath(_regionBtn));
            regionSelection.Click();
            Thread.Sleep(1000);
            var regionNew = _driver.FindElement(By.XPath(_regionNew));
            regionNew.Click();
            Thread.Sleep(1000);
            var regionConfirm = _driver.FindElement(By.XPath(_regionConfirm));
            regionConfirm.Click();
            Thread.Sleep(1000);

            return this;
        }

        public string GetRegion()
        {
            Thread.Sleep(1000);
            var regionSelection = _driver.FindElement(By.XPath(_regionBtn));

            return regionSelection.Text;
        }
    }
}
