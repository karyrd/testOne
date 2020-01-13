using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TestFramework.Constants;
using TestFramework.Page.Base;

namespace TestFramework.Page
{
    public class ItemsPage : SearchBarPageBase<ItemsPage>
    {
        private const string _ItemCards = "products-catalog__list";
        private const string _filterId = "";
        private const string _productPricesSelector = "multifilter_price";
        private const string _noItemsFound = "//*[@id='vue-root']/div[5]/div[2]/div[2]/div/div/div[3]/div/h2";
        private const string _productsComparisonSelector = "";
        private const string _checkboxSelector = "";

        public ItemsPage(IWebDriver driver)
            : base(driver)
        {
            Url = PageUrl.Items;
        }

        public ItemPage OpenItem()
        {
            var ItemCard = _driver.FindElements(By.ClassName(_ItemCards)).First().FindElement(By.ClassName("products-list-item__link"));
            ItemCard.Click();
            Thread.Sleep(500);

            return new ItemPage(_driver);
        }

        public List<string> GetBrandNames()
        {
            List<string> brands = new List<string>();
            var ItemCards = _driver.FindElements(By.ClassName(_ItemCards));
            foreach (var element in ItemCards)
                brands.Add(element.FindElement(By.ClassName("products-list-item__brand-name")).Text.ToLower());
            return brands;
        }

        public List<string> GetItemsTypes()
        {
            List<string> types = new List<string>();
            var ItemCards = _driver.FindElements(By.ClassName(_ItemCards));
            foreach (var element in ItemCards)
                types.Add(element.FindElement(By.ClassName("products-list-item__type")).Text.ToLower());
            return types;
        }

        public bool PricesFilterCheck(int leftValue, int rightValue)
        {
            bool result = true;
            var dropdownPrice = _driver.FindElement(By.ClassName(_productPricesSelector));
            dropdownPrice.Click();
            dropdownPrice.FindElement(By.ClassName("range__value_left")).SendKeys(leftValue.ToString());
            dropdownPrice.FindElement(By.ClassName("range__value_right")).SendKeys(rightValue.ToString());
            dropdownPrice.FindElement(By.ClassName("multifilter-actions__apply")).Click();
            Thread.Sleep(1000);

            try
            {
                var filterResultItems = _driver.FindElements(By.ClassName("price__actual"));
                string price = "";
                foreach (var filterResultItem in filterResultItems)
                {
                    price = "";
                    for (int i = 0; i < filterResultItem.Text.Length - 3; i++)
                        price += filterResultItem.Text[i];
                    if (int.Parse(price) < leftValue || int.Parse(price) > rightValue)
                    {
                        result = false;
                        break;
                    }
                }
            }
            catch
            {
                return (_driver.FindElement(By.ClassName(_noItemsFound)).Text == "В выбранной категории ничего не найдено"
                    ? true : false);
            }

            return result;
        }

        public ItemsPage ApplyMaxPriceFilter(double max)
        {
            Thread.Sleep(5000);
            var filter = _driver.FindElement(By.Id(_filterId));
            filter.Click();
            filter.SendKeys(max.ToString());
            filter.Submit();

            return this;
        }

        public ItemsPage ApplyManufacturerFilter(string manufacturer)
        {
            Thread.Sleep(5000);
            var checkboxes = _driver.FindElements(By.CssSelector(_checkboxSelector));
            foreach (var checkbox in checkboxes)
            {
                var checkboxElement = checkbox.FindElement(By.XPath("//label"));
                if (checkboxElement.GetAttribute("#text").ToLower().Contains(manufacturer.ToLower()))
                {
                    checkboxElement.Click();
                    break;
                }
            }

            return this;
        }

        public List<double> GetPrices()
        {
            Thread.Sleep(5000);
            var productPricesBlocks = _driver.FindElements(By.CssSelector(_productPricesSelector));
            var productPrices = new List<double>();
            foreach (var productPriceBlock in productPricesBlocks)
            {
                var productPriceString = productPriceBlock.GetAttribute("innerHTML");
                productPriceString = productPriceString.Replace(" р.", "");
                productPriceString = productPriceString.Replace('.', ',');
                productPrices.Add(double.Parse(productPriceString));
            }

            return productPrices;
        }

        public ItemsPage AddFirstProduct()
        {
            Thread.Sleep(5000);
            var products = _driver.FindElements(By.CssSelector(_productsComparisonSelector));
            products[0].Click();

            return this;
        }
    }
}
