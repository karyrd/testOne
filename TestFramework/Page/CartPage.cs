using OpenQA.Selenium;
using TestFramework.Test.Base;
using System.Threading;
using OpenQA.Selenium.Interactions;

namespace TestFramework.Page
{
    public class CartPage : PageBase
    {
        private const string _productInCartTitle = "//*[@id='checkout__cart']/div/div[2]/div/div/div[1]/div[2]/div[1]/b";
        private const string _productInCartPrice = "//*[@id='checkout__cart']/div/div[2]/div/div/div[3]/div[1]/span";
        private const string _productInCartDelete = "/html/body/div[1]/div[5]/div[3]/div[2]/div[2]/div/div[2]/div/div/div[3]/div[2]/button";
        private const string _emptyCart = "//*[@id='checkout__empty']/h2";

        public CartPage(IWebDriver driver)
            : base(driver)
        { }

        public string GetTitle()
        {
            var itemTitle = _driver.FindElement(By.XPath(_productInCartTitle));
            return itemTitle.Text;
        }

        public string GetPrice()
        {
            var itemPrice = _driver.FindElement(By.XPath(_productInCartPrice));
            return itemPrice.Text;
        }

        public string EmptyCartText()
        {
            Thread.Sleep(3000);
            var emptyCartLabel = _driver.FindElement(By.XPath(_emptyCart));
            return emptyCartLabel.Text;
        }

        public CartPage EmptyCart()
        {
            Thread.Sleep(1000);
            var emptyCartBtn = _driver.FindElement(By.XPath(_productInCartDelete));
            ScrollToElement(emptyCartBtn);
            Thread.Sleep(1000);
            emptyCartBtn.Click();
            Thread.Sleep(1000);
            emptyCartBtn.Click();
            Thread.Sleep(1000);
            emptyCartBtn.Click();

            return this;
        }
    }
}
