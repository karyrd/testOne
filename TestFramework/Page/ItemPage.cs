using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Threading;
using TestFramework.Constants;
using TestFramework.Test.Base;

namespace TestFramework.Page
{
    public class ItemPage : PageBase
    {
        private const string _addToWishButton = "//*[@id='vue-root']/div[3]/div/div/div[1]/div/div[2]/div/div[4]/span";
        private const string _sizeDropdown = "product__sizes-select-container";
        private const string _sizeOptionsList = "//*[@id='vue-root']/div[3]/div/div/div[1]/div/div[2]/div/div[3]/div[1]/div/div/div[3]/div[2]/div[2]/div/div[1]";
        private const string _addToCartButton = "product__cart-add-button";
        private const string _goToCartButton = "post-cart-add__basket";
        private const string _itemName = "//*[@id='vue-root']/div[3]/div/div/div[1]/div/h1/span/span/a";
        private const string _itemPrice = "//*[@id='vue-root']/div[3]/div/div/div[1]/div/div[2]/div/div[1]/div/span[3]";
        private const string _itemVendorCode = "//*[@id='vue-root']/div[3]/div/div/div[1]/div/div[4]/div[2]/div[2]/div[18]/span[2]";

        private const string _itemCommentStart = "//*[@id='reviews-and-questions']/div[3]/div[3]/div[1]/div[1]/div[2]/div";
        private const string _itemCommentRating = "/html/body/div[15]/div/div/div/div[2]/form/div[2]/div[1]/span[2]/fieldset/label[1]";
        private const string _itemCommentTextInput = "/html/body/div[15]/div/div/div/div[2]/form/div[2]/div[2]/textarea";
        private const string _itemCommentSubmitButton = "/html/body/div[15]/div/div/div/div[2]/form/div[4]/input";
        private const string _itemCommentSuccess = "/html/body/div[11]/div/div/div/div[2]/div[1]";

        public ItemPage(IWebDriver driver)
            : base(driver)
        {
            Url = PageUrl.Items;
        }

        public string GetItemName()
        {
            var itemName = _driver.FindElement(By.XPath(_itemName));
            return itemName.Text;
        }

        public string GetItemPrice()
        {
            var itemPrice = _driver.FindElement(By.XPath(_itemPrice));
            return itemPrice.Text;
        }

        public string GetItemVendorCode()
        {
            var itemVendorCode = _driver.FindElement(By.XPath(_itemVendorCode));
            return itemVendorCode.Text;
        }

        public ItemPage AddToWishlist()
        {
            Thread.Sleep(1000);
            var addToWishButton = _driver.FindElement(By.XPath(_addToWishButton));
            addToWishButton.Click();
            Thread.Sleep(1000);
            return this;
        }

        public ItemPage AddToCart()
        {
            var sizeDropdown = _driver.FindElement(By.ClassName(_sizeDropdown));
            Thread.Sleep(500);
            sizeDropdown.Click();
            Thread.Sleep(500);
            var sizeOptions = _driver.FindElement(By.XPath(_sizeOptionsList));
            foreach(var sizeOption in sizeOptions.FindElements(By.ClassName("ii-select__option")))
            {
                if (!sizeOption.GetAttribute("class").Contains("ii-select__option_disabled"))
                {
                    sizeOption.Click();
                    break;
                }
            }
            Thread.Sleep(500);

            var addToCartButton = _driver.FindElement(By.ClassName(_addToCartButton));
            addToCartButton.Click();
            Thread.Sleep(3000);

            return this;
        }

        public CartPage GoToCartPage()
        {
            var goToCartButton = _driver.FindElement(By.ClassName(_goToCartButton));
            goToCartButton.Click();

            return new CartPage(_driver);
        }

        public ItemPage LeaveComment(string comment)
        {
            var commentStart = _driver.FindElement(By.XPath(_itemCommentStart));
            commentStart.Click();
            Thread.Sleep(500);
            var commentRating = _driver.FindElement(By.XPath(_itemCommentRating));
            commentRating.Click();
            Thread.Sleep(500);
            var commentText = _driver.FindElement(By.XPath(_itemCommentTextInput));
            commentText.SendKeys(comment);
            Thread.Sleep(500);
            var commentSubmit = _driver.FindElement(By.XPath(_itemCommentSubmitButton));
            commentSubmit.Click();
            Thread.Sleep(2000);

            return this;
        }

        public string LeaveCommentResult()
        {
            var commentResult = _driver.FindElement(By.XPath(_itemCommentSuccess));
            return commentResult.Text;
        }
    }
}
