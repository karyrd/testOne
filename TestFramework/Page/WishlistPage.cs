using OpenQA.Selenium;
using System.Threading;
using TestFramework.Constants;
using TestFramework.Test.Base;

namespace TestFramework.Page
{
    public class WishlistPage : PageBase
    {
        private const string _itemName = "/html/body/div[1]/div[5]/div/div[2]/div/div/div/div[2]/div[3]/div/div/a/div[4]/span[1]";
        private const string _itemHeart = "/html/body/div[1]/div[5]/div/div[2]/div/div/div/div[2]/div[3]/div/div/a/div[1]";

        public WishlistPage(IWebDriver driver)
            : base(driver)
        {
            Url = PageUrl.Wishlist;
        }

        public string GetItemName()
        {
            var itemName = _driver.FindElement(By.XPath(_itemName));
            return itemName.Text;
        }

        public WishlistPage RemoveFromWishlist()
        {
            var itemHeart = _driver.FindElement(By.XPath(_itemHeart));
            itemHeart.Click();

            return this;
        }
    }
}
