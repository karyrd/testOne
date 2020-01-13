using OpenQA.Selenium;
using System.Threading;
using TestFramework.Test.Base;

namespace TestFramework.Page.Base
{
    public class SearchBarPageBase<T> : PageBase
        where T : SearchBarPageBase<T>
    {
        public SearchBarPageBase(IWebDriver driver)
            : base(driver)
        {
            _driver = driver;
        }

        private const string _searchBar = "//*[@id='menu-wrapper']/div/div[1]/input";
        private const string _searchButton = "//*[@id='menu-wrapper']/div/div[1]/div[2]";

        public T SearchProduct(string productTitle)
        {
            var searchBar = _driver.FindElement(By.XPath(_searchBar));
            var searchButton = _driver.FindElement(By.XPath(_searchButton));
            searchBar.Clear();
            searchBar.SendKeys(productTitle);
            searchButton.Click();
            Thread.Sleep(500);

            return (T)this;
        }
    }
}
